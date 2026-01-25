using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetQueries;
using EnvanteriX.Application.Features.Results.AssetResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnvanteriX.Application.Features.Handlers.AssetHandlers
{
    /// <summary>
    /// Sayfalama ve filtreleme destekli varlık listesi sorgu işleyicisi
    /// </summary>
    public class GetAllAssetsPaginatedQueryHandler : BaseHandler, 
        IRequestHandler<GetAllAssetsPaginatedQuery, PaginatedList<GetAllAssetsQueryResult>>
    {
        public GetAllAssetsPaginatedQueryHandler(
            IMapper mapper, 
            IUnitOfWork unitOfWork, 
            IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<PaginatedList<GetAllAssetsQueryResult>> Handle(
            GetAllAssetsPaginatedQuery request, 
            CancellationToken cancellationToken)
        {
            // Filtreleme için predicate oluştur
            Expression<Func<Asset, bool>> predicate = BuildPredicate(request);

            // Toplam kayıt sayısını al
            var totalCount = await _unitOfWork.GetReadRepository<Asset>()
                .CountAsync(predicate);

            // Sayfalanmış veriyi al
            var assets = await _unitOfWork.GetReadRepository<Asset>().GetAllByPagingAsync(
                predicate: predicate,
                include: x => x.Include(c => c.AssetType)
                    .Include(b => b.Model).ThenInclude(b => b.Brand)
                    .Include(b => b.Vendor)
                    .Include(b => b.Location)
                    .Include(b => b.AssignedUser)
                    .Include(c => c.AssignedDepartment),
                orderby: x => x.OrderByDescending(a => a.Id),
                enableTracking: false,
                currentPage: request.PageNumber,
                pageSize: request.PageSize
            );

            // DTO'ya map et
            var mappedAssets = _mapper.Map<GetAllAssetsQueryResult, Asset>(assets, config: cfg =>
            {
                cfg.CreateMap<Asset, GetAllAssetsQueryResult>()
                   .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.AssetType != null ? src.AssetType.TypeName : "-"))
                   .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model != null ? src.Model.ModelName : "-"))
                   .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Model != null && src.Model.Brand != null ? src.Model.Brand.BrandName : "-"))
                   .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor != null ? src.Vendor.VendorName : "-"))
                   .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? $"{src.Location.Building}" : "-"))
                   .ForMember(dest => dest.AssignedUserName, opt => opt.MapFrom(src => src.AssignedUser != null ? src.AssignedUser.FullName : "-"))
                   .ForMember(dest => dest.AssignedDepartmentName, opt => opt.MapFrom(src => src.AssignedDepartment != null ? src.AssignedDepartment.Name : "-"))
                   .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.Model != null && src.Model.Brand != null ? src.Model.Brand.Id : 0));
            });

            // PaginatedList oluştur ve döndür
            var paginatedList = new PaginatedList<GetAllAssetsQueryResult>(
                mappedAssets.ToList(),
                totalCount,
                request.PageNumber,
                request.PageSize
            );

            return paginatedList;
        }

        /// <summary>
        /// Filtreleme parametrelerine göre dinamik predicate oluşturur
        /// </summary>
        private Expression<Func<Asset, bool>> BuildPredicate(GetAllAssetsPaginatedQuery request)
        {
            // Başlangıç predicate'i - her zaman true
            Expression<Func<Asset, bool>> predicate = x => true;

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

            // SearchTerm - AssetTag veya SerialNumber'da arama
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.Trim().ToLower();
                predicate = predicate.And(x => 
                    (x.AssetTag != null && x.AssetTag.ToLower().Contains(searchTerm)) || 
                    x.SerialNumber.ToLower().Contains(searchTerm));
            }

            // AssetTag filtresi
            if (!string.IsNullOrWhiteSpace(request.AssetTag))
            {
                var assetTag = request.AssetTag.Trim().ToLower();
                predicate = predicate.And(x => x.AssetTag != null && x.AssetTag.ToLower().Contains(assetTag));
            }

            // SerialNumber filtresi
            if (!string.IsNullOrWhiteSpace(request.SerialNumber))
            {
                var serialNumber = request.SerialNumber.Trim().ToLower();
                predicate = predicate.And(x => x.SerialNumber.ToLower().Contains(serialNumber));
            }

            // AssetTypeId filtresi
            if (request.AssetTypeId.HasValue)
            {
                var assetTypeId = request.AssetTypeId.Value;
                predicate = predicate.And(x => x.AssetTypeId == assetTypeId);
            }

            // BrandId filtresi
            if (request.BrandId.HasValue)
            {
                var brandId = request.BrandId.Value;
                predicate = predicate.And(x => x.Model.BrandId == brandId);
            }

            // ModelId filtresi
            if (request.ModelId.HasValue)
            {
                var modelId = request.ModelId.Value;
                predicate = predicate.And(x => x.ModelId == modelId);
            }

            // LocationId filtresi
            if (request.LocationId.HasValue)
            {
                var locationId = request.LocationId.Value;
                predicate = predicate.And(x => x.LocationId == locationId);
            }

            // VendorId filtresi
            if (request.VendorId.HasValue)
            {
                var vendorId = request.VendorId.Value;
                predicate = predicate.And(x => x.VendorId == vendorId);
            }

            // AssignedUserId filtresi
            if (request.AssignedUserId.HasValue)
            {
                var userId = request.AssignedUserId.Value;
                predicate = predicate.And(x => x.AssignedUserId == userId);
            }

            // AssignedDepartmentId filtresi
            if (request.AssignedDepartmentId.HasValue)
            {
                var deptId = request.AssignedDepartmentId.Value;
                predicate = predicate.And(x => x.AssignedDepartmentId == deptId);
            }

            // Status filtresi
            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                var status = request.Status.Trim();
                predicate = predicate.And(x => x.Status.ToString() == status);
            }

            // IsRented filtresi
            if (request.IsRented.HasValue)
            {
                var isRented = request.IsRented.Value;
                predicate = predicate.And(x => x.IsRented == isRented);
            }

            return predicate;
        }
    }

    /// <summary>
    /// Expression birleştirme için yardımcı extension metodlar
    /// </summary>
    public static class PredicateExtensions
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
