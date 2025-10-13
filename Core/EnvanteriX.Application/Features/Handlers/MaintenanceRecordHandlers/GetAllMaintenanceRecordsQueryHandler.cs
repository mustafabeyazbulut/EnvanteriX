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
    public class GetAllMaintenanceRecordsQueryHandler : BaseHandler, IRequestHandler<GetAllMaintenanceRecordsQuery, List<GetAllMaintenanceRecordsQueryResult>>
    {
        public GetAllMaintenanceRecordsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor) { }

        public async Task<List<GetAllMaintenanceRecordsQueryResult>> Handle(GetAllMaintenanceRecordsQuery request, CancellationToken cancellationToken)
        {
            var models = await _unitOfWork.GetReadRepository<MaintenanceRecord>().GetAllAsync(
                include: x => x.Include(m => m.Asset).ThenInclude(m=>m.Model).ThenInclude(x=>x.Brand)
                            .Include(m => m.Vendor)
                );
            var map = _mapper.Map<GetAllMaintenanceRecordsQueryResult, MaintenanceRecord>(models, config: cfg =>
            {
                cfg.CreateMap<MaintenanceRecord, GetAllMaintenanceRecordsQueryResult>()
                   .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => $"Marka: {src.Asset.Model.Brand.BrandName}, " +
                    $"Model: {src.Asset.Model.ModelName}, SeriNo: {src.Asset.SerialNumber}, Key: {src.Asset.AssetTag}"))
                     .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor != null ? src.Vendor.VendorName : "Bilinmiyor"));
            });
            return map.ToList();
        }
    }
}
