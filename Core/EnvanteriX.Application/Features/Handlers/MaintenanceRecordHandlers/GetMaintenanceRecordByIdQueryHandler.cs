using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.MaintenanceRecordQueries;
using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;
using EnvanteriX.Application.Features.Rules.MaintenanceRecordRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.MaintenanceRecordHandlers
{
    public class GetMaintenanceRecordByIdQueryHandler : BaseHandler, IRequestHandler<GetMaintenanceRecordByIdQuery, GetMaintenanceRecordByIdQueryResult>
    {
        private readonly MaintenanceRecordRules _maintenanceRecordRules;
        public GetMaintenanceRecordByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, MaintenanceRecordRules maintenanceRecordRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _maintenanceRecordRules = maintenanceRecordRules;
        }
        public async Task<GetMaintenanceRecordByIdQueryResult> Handle(GetMaintenanceRecordByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<MaintenanceRecord>().GetAsync(
                predicate: x => x.Id == request.Id,
              include: x => x.Include(m => m.Asset)
                          .Include(m => m.Vendor)
              );
            await _maintenanceRecordRules.MaintenanceRecordShouldExist(model);

            var map = _mapper.Map<GetMaintenanceRecordByIdQueryResult, MaintenanceRecord>(model, config: cfg =>
            {
                cfg.CreateMap<MaintenanceRecord, GetMaintenanceRecordByIdQueryResult>()
                   .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => $"Marka: {src.Asset.Model.Brand.BrandName}, " +
                    $"Model: {src.Asset.Model.ModelName}, SeriNo: {src.Asset.SerialNumber}, Key: {src.Asset.AssetTag}"))
                     .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor != null ? src.Vendor.VendorName : "Bilinmiyor"));
            });
            return map;
        }
    }
}
