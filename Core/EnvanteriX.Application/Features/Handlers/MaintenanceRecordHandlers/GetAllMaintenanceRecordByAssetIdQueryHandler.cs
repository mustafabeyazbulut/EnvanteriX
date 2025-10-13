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
    public class GetAllMaintenanceRecordByAssetIdQueryHandler : BaseHandler, IRequestHandler<GetAllMaintenanceRecordByAssetIdQuery, List<GetAllMaintenanceRecordByAssetIdQueryResult>>
    {
        public GetAllMaintenanceRecordByAssetIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllMaintenanceRecordByAssetIdQueryResult>> Handle(GetAllMaintenanceRecordByAssetIdQuery request, CancellationToken cancellationToken)
        {
            var models = await _unitOfWork.GetReadRepository<MaintenanceRecord>().GetAllAsync(
                predicate: x => x.AssetId == request.Id,
                include: x => x.Include(m => m.Asset)
                                   .ThenInclude(m => m.Model)
                                   .ThenInclude(x => x.Brand)
                               .Include(m => m.Vendor)
                );
            var map = _mapper.Map<GetAllMaintenanceRecordByAssetIdQueryResult, MaintenanceRecord>(models, config: cfg =>
            {
                cfg.CreateMap<MaintenanceRecord, GetAllMaintenanceRecordByAssetIdQueryResult>()
                   .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => $"Marka: {src.Asset.Model.Brand.BrandName}, " +
                    $"Model: {src.Asset.Model.ModelName}, SeriNo: {src.Asset.SerialNumber}, Key: {src.Asset.AssetTag}"))
                     .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor != null ? src.Vendor.VendorName : "Bilinmiyor"));
            });
            return map.ToList();
        }
    }
}
