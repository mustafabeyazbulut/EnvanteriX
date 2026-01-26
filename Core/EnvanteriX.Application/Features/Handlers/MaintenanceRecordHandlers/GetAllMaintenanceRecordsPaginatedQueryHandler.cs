using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.MaintenanceRecordQueries;
using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnvanteriX.Application.Features.Handlers.MaintenanceRecordHandlers
{
    /// <summary>
    /// Sayfalama ve filtreleme destekli bakım kayıtları listesi sorgu işleyicisi
    /// </summary>
    public class GetAllMaintenanceRecordsPaginatedQueryHandler : BaseHandler,
        IRequestHandler<GetAllMaintenanceRecordsPaginatedQuery, PaginatedList<GetAllMaintenanceRecordsQueryResult>>
    {
        public GetAllMaintenanceRecordsPaginatedQueryHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<PaginatedList<GetAllMaintenanceRecordsQueryResult>> Handle(
            GetAllMaintenanceRecordsPaginatedQuery request,
            CancellationToken cancellationToken)
        {
            // Filtreleme için predicate oluştur
            Expression<Func<MaintenanceRecord, bool>> predicate = BuildPredicate(request);

            // Toplam kayıt sayısını al
            var totalCount = await _unitOfWork.GetReadRepository<MaintenanceRecord>()
                .CountAsync(predicate);

            // Sayfalanmış veriyi al
            var records = await _unitOfWork.GetReadRepository<MaintenanceRecord>().GetAllByPagingAsync(
                predicate: predicate,
                include: x => x.Include(m => m.Asset)
                    .ThenInclude(a => a.AssetType)
                    .Include(m => m.Asset)
                    .ThenInclude(a => a.Model)
                    .ThenInclude(m => m.Brand)
                    .Include(m => m.Asset)
                    .ThenInclude(a => a.Location)
                    .Include(m => m.Vendor),
                orderby: x => x.OrderByDescending(m => m.Id),
                enableTracking: false,
                currentPage: request.PageNumber,
                pageSize: request.PageSize
            );

            // DTO'ya map et
            var mappedRecords = _mapper.Map<GetAllMaintenanceRecordsQueryResult, MaintenanceRecord>(records, config: cfg =>
            {
                cfg.CreateMap<MaintenanceRecord, GetAllMaintenanceRecordsQueryResult>()
                   .ForMember(dest => dest.AssetTag, opt => opt.MapFrom(src => src.Asset != null ? src.Asset.AssetTag : "-"))
                   .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset != null && src.Asset.Model != null && src.Asset.Model.Brand != null 
                       ? $"{src.Asset.Model.Brand.BrandName} {src.Asset.Model.ModelName}" 
                       : (src.Asset != null ? src.Asset.SerialNumber : "-")))
                   .ForMember(dest => dest.AssetTypeName, opt => opt.MapFrom(src => src.Asset != null && src.Asset.AssetType != null ? src.Asset.AssetType.TypeName : "-"))
                   .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Asset != null && src.Asset.Location != null ? src.Asset.Location.Building : "-"))
                   .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor != null ? src.Vendor.VendorName : "-"))
                   .ForMember(dest => dest.DurationDays, opt => opt.MapFrom(src =>
                       src.EndDate.HasValue ? (src.EndDate.Value - src.StartDate).Days : (int?)null))
                   .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.EndDate.HasValue));
            });

            // PaginatedList oluştur ve döndür
            var paginatedList = new PaginatedList<GetAllMaintenanceRecordsQueryResult>(
                mappedRecords.ToList(),
                totalCount,
                request.PageNumber,
                request.PageSize
            );

            return paginatedList;
        }

        /// <summary>
        /// Filtreleme parametrelerine göre dinamik predicate oluşturur
        /// </summary>
        private Expression<Func<MaintenanceRecord, bool>> BuildPredicate(GetAllMaintenanceRecordsPaginatedQuery request)
        {
            // Başlangıç predicate'i - her zaman true
            Expression<Func<MaintenanceRecord, bool>> predicate = x => true;

            // IsDeleted filtresi - varsayılan olarak aktif kayıtlar
            if (request.IsDeleted.HasValue)
            {
                var isDeleted = request.IsDeleted.Value;
                predicate = predicate.And(x => x.IsDeleted == isDeleted);
            }
            else
            {
                predicate = predicate.And(x => !x.IsDeleted);
            }

            // SearchTerm - Asset tag, serial number veya açıklamada arama
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.Trim().ToLower();
                predicate = predicate.And(x =>
                    (x.Asset.AssetTag != null && x.Asset.AssetTag.ToLower().Contains(searchTerm)) ||
                    x.Asset.SerialNumber.ToLower().Contains(searchTerm) ||
                    x.PreServiceDescription.ToLower().Contains(searchTerm) ||
                    (x.PostServiceDescription != null && x.PostServiceDescription.ToLower().Contains(searchTerm)));
            }

            // AssetId filtresi
            if (request.AssetId.HasValue)
            {
                var assetId = request.AssetId.Value;
                predicate = predicate.And(x => x.AssetId == assetId);
            }

            // VendorId filtresi
            if (request.VendorId.HasValue)
            {
                var vendorId = request.VendorId.Value;
                predicate = predicate.And(x => x.VendorId == vendorId);
            }

            // StartDateFrom filtresi
            if (request.StartDateFrom.HasValue)
            {
                var startDateFrom = request.StartDateFrom.Value;
                predicate = predicate.And(x => x.StartDate >= startDateFrom);
            }

            // StartDateTo filtresi
            if (request.StartDateTo.HasValue)
            {
                var startDateTo = request.StartDateTo.Value;
                predicate = predicate.And(x => x.StartDate <= startDateTo);
            }

            // IsCompleted filtresi
            if (request.IsCompleted.HasValue)
            {
                if (request.IsCompleted.Value)
                {
                    predicate = predicate.And(x => x.EndDate != null);
                }
                else
                {
                    predicate = predicate.And(x => x.EndDate == null);
                }
            }

            return predicate;
        }
    }

    /// <summary>
    /// Expression birleştirme için yardımcı extension metodlar
    /// </summary>
    public static class MaintenancePredicateExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(first.Parameters[0], parameter);
            var left = leftVisitor.Visit(first.Body);

            var rightVisitor = new ReplaceExpressionVisitor(second.Parameters[0], parameter);
            var right = rightVisitor.Visit(second.Body);

            if (left == null || right == null)
                throw new InvalidOperationException("Expression visit returned null");

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left, right), parameter);
        }

        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression? Visit(Expression? node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }
    }
}
