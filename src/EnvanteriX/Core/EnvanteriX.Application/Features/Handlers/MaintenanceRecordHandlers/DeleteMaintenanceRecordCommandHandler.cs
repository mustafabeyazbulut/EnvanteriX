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
    public class DeleteMaintenanceRecordCommandHandler : BaseHandler, IRequestHandler<DeleteMaintenanceRecordCommand, Unit>
    {
        private readonly MaintenanceRecordRules _maintenanceRecordRules;
        public DeleteMaintenanceRecordCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, MaintenanceRecordRules maintenanceRecordRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _maintenanceRecordRules = maintenanceRecordRules;
        }
        public async Task<Unit> Handle(DeleteMaintenanceRecordCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<MaintenanceRecord>().GetAsync(x => x.Id == request.Id );
            await _maintenanceRecordRules.MaintenanceRecordShouldExist(model);
            await _unitOfWork.GetWriteRepository<MaintenanceRecord>().HardDeleteAsync(model);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
