using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.MaintenanceRecordQueries;
using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.MaintenanceRecordHandlers
{
    public class GetMaintenanceRecordByIdQueryHandler : BaseHandler,
        IRequestHandler<GetMaintenanceRecordByIdQuery, GetMaintenanceRecordByIdQueryResult>
    {
        public GetMaintenanceRecordByIdQueryHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<GetMaintenanceRecordByIdQueryResult> Handle(
            GetMaintenanceRecordByIdQuery request,
            CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.GetReadRepository<MaintenanceRecord>().GetAsync(
                predicate: x => x.Id == request.Id,
                include: x => x.Include(m => m.Asset)
                    .ThenInclude(a => a.AssetType)
                    .Include(m => m.Asset)
                    .ThenInclude(a => a.Model)
                    .ThenInclude(m => m.Brand)
                    .Include(m => m.Asset)
                    .ThenInclude(a => a.Location)
                    .Include(m => m.Vendor)
            );

            if (record == null) return null;

            var result = _mapper.Map<GetMaintenanceRecordByIdQueryResult, MaintenanceRecord>(record, config: cfg =>
            {
                cfg.CreateMap<MaintenanceRecord, GetMaintenanceRecordByIdQueryResult>()
                   .ForMember(dest => dest.AssetTag, opt => opt.MapFrom(src => src.Asset.AssetTag))
                   .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => 
                       $"{src.Asset.Model.Brand.BrandName} {src.Asset.Model.ModelName}"))
                   .ForMember(dest => dest.AssetType, opt => opt.MapFrom(src => src.Asset.AssetType.TypeName))
                   .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Asset.Model.Brand.BrandName))
                   .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Asset.Model.ModelName))
                   .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Asset.Location != null ? src.Asset.Location.Building : "-"))
                   .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.Asset.SerialNumber))
                   .ForMember(dest => dest.AssetStatus, opt => opt.MapFrom(src => src.Asset.Status.ToString()))
                   .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor.VendorName))
                   .ForMember(dest => dest.VendorPhone, opt => opt.MapFrom(src => src.Vendor.PhoneNumber))
                   .ForMember(dest => dest.VendorEmail, opt => opt.MapFrom(src => src.Vendor.Email))
                   .ForMember(dest => dest.DurationDays, opt => opt.MapFrom(src =>
                       src.EndDate.HasValue ? (src.EndDate.Value - src.StartDate).Days : (int?)null))
                   .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.EndDate.HasValue));
            });

            return result;
        }
    }
}
