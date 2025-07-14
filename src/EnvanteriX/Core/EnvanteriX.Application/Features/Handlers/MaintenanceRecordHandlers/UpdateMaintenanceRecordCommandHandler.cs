using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands;
using EnvanteriX.Application.Features.Rules.MaintenanceRecordRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.MaintenanceRecordHandlers
{
    public class UpdateMaintenanceRecordCommandHandler : BaseHandler, IRequestHandler<UpdateMaintenanceRecordCommand, Unit>
    {
        private readonly MaintenanceRecordRules _maintenanceRecordRules;
        public UpdateMaintenanceRecordCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, MaintenanceRecordRules maintenanceRecordRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _maintenanceRecordRules = maintenanceRecordRules;
        }
        public async Task<Unit> Handle(UpdateMaintenanceRecordCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<MaintenanceRecord>().GetAsync(x => x.Id == request.Id);
            await _maintenanceRecordRules.MaintenanceRecordShouldExist(model);

            model.AssetId = request.AssetId;
            model.PerformedBy = request.PerformedBy;
            model.Cost = request.Cost;
            model.VendorId = request.VendorId;
            model.PreServiceDescription = request.PreServiceDescription;
            model.PostServiceDescription = request.PostServiceDescription;
            await _unitOfWork.GetWriteRepository<MaintenanceRecord>().UpdateAsync(model);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
