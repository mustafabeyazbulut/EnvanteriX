using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands;
using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.MaintenanceRecordHandlers
{
    public class CreateMaintenanceRecordCommandHandler : BaseHandler, IRequestHandler<CreateMaintenanceRecordCommand, CreateMaintenanceRecordCommandResult>
    {
        public CreateMaintenanceRecordCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor) { }

        public async Task<CreateMaintenanceRecordCommandResult> Handle(CreateMaintenanceRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = new MaintenanceRecord
            {
                AssetId = request.AssetId,
                MaintenanceDate = request.MaintenanceDate,
                PerformedBy = request.PerformedBy,
                Description = request.Description,
                Cost = request.Cost,
                VendorId = request.VendorId
            };

            await _unitOfWork.GetWriteRepository<MaintenanceRecord>().AddAsync(entity);
            await _unitOfWork.SaveAsync();

            return new CreateMaintenanceRecordCommandResult
            {
                Id = entity.Id,
                AssetId = entity.AssetId
            };
        }
    }
}
