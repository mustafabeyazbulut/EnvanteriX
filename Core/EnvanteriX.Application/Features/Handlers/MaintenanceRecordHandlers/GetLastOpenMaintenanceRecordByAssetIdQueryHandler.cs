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
    public class GetLastOpenMaintenanceRecordByAssetIdQueryHandler
        : BaseHandler, IRequestHandler<GetLastOpenMaintenanceRecordByAssetIdQuery, GetLastOpenMaintenanceRecordByAssetIdQueryResult?>
    {
        public GetLastOpenMaintenanceRecordByAssetIdQueryHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<GetLastOpenMaintenanceRecordByAssetIdQueryResult?> Handle(
            GetLastOpenMaintenanceRecordByAssetIdQuery request,
            CancellationToken cancellationToken)
        {
            // Sadece açık (EndDate == null) olan son bakım kaydını getir
            var maintenance = await _unitOfWork
                .GetReadRepository<MaintenanceRecord>()
                .GetAsync(
                    predicate: x => x.AssetId == request.Id && x.EndDate == null,
                    include: x => x
                        .Include(m => m.Asset)
                            .ThenInclude(a => a.Model)
                            .ThenInclude(m => m.Brand)
                        .Include(m => m.Vendor),
                    orderBy: q => q.OrderByDescending(x => x.StartDate)
                );

            if (maintenance == null)
                return null;

            // Tek kaydı map et
            var map = _mapper.Map<GetLastOpenMaintenanceRecordByAssetIdQueryResult, MaintenanceRecord>(maintenance, config: cfg =>
            {
                cfg.CreateMap<MaintenanceRecord, GetAllMaintenanceRecordsQueryResult>()
                   .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => $"Marka: {src.Asset.Model.Brand.BrandName}, " +
                    $"Model: {src.Asset.Model.ModelName}, SeriNo: {src.Asset.SerialNumber}, Key: {src.Asset.AssetTag}"))
                     .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor != null ? src.Vendor.VendorName : "Bilinmiyor"));
            });
            map.VendorName = maintenance.Vendor != null ? maintenance.Vendor.VendorName : "Bilinmiyor";

            return map;
        }
    }
}
