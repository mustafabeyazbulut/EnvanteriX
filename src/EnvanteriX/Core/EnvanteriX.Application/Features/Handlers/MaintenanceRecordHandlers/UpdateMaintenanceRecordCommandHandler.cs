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
    public class UpdateMaintenanceRecordCommandHandler : BaseHandler, IRequestHandler<UpdateMaintenanceRecordCommand, UpdateMaintenanceRecordCommandResult>
    {
        public UpdateMaintenanceRecordCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor) { }

        public async Task<UpdateMaintenanceRecordCommandResult> Handle(UpdateMaintenanceRecordCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetReadRepository<MaintenanceRecord>();
            var entity = await repository.GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (entity == null) 
                throw new Exception("Maintenance record not found or has been deleted.");

            entity.AssetId = request.AssetId;
            entity.MaintenanceDate = request.MaintenanceDate;
            entity.PerformedBy = request.PerformedBy;
            entity.Description = request.Description;
            entity.Cost = request.Cost;
            entity.VendorId = request.VendorId;

          await  _unitOfWork.GetWriteRepository<MaintenanceRecord>().UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return new UpdateMaintenanceRecordCommandResult
            {
                Id = entity.Id,
                AssetId = entity.AssetId
            };
        }
    }
}
