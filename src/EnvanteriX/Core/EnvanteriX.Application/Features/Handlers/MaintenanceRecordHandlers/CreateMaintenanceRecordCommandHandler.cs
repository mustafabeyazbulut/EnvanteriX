using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands;
using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;
using EnvanteriX.Application.Features.Rules.MaintenanceRecordRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.MaintenanceRecordHandlers
{
    public class CreateMaintenanceRecordCommandHandler : BaseHandler, IRequestHandler<CreateMaintenanceRecordCommand, CreateMaintenanceRecordCommandResult>
    {
        private readonly MaintenanceRecordRules _maintenanceRecordRules;
        public CreateMaintenanceRecordCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, MaintenanceRecordRules maintenanceRecordRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _maintenanceRecordRules = maintenanceRecordRules;
        }

        public async Task<CreateMaintenanceRecordCommandResult> Handle(CreateMaintenanceRecordCommand request, CancellationToken cancellationToken)
        {
            var existingRecord = await _unitOfWork.GetReadRepository<MaintenanceRecord>().GetAsync(m =>
                            m.AssetId == request.AssetId &&
                            (m.EndDate == null || m.EndDate == DateTime.MinValue)
                        );
            await _maintenanceRecordRules.MaintenanceRecordAlreadyExists(existingRecord);

            var model= _mapper.Map<MaintenanceRecord, CreateMaintenanceRecordCommand>(request);

            model.StartDate = DateTime.UtcNow;
           
            await _unitOfWork.GetWriteRepository<MaintenanceRecord>().AddAsync(model);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<CreateMaintenanceRecordCommandResult, MaintenanceRecord>(model);
        }
    }
}
