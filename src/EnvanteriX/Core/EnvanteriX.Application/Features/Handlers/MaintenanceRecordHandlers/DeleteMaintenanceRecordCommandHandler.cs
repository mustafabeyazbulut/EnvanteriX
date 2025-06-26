using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.MaintenanceRecordHandlers
{
    public class DeleteMaintenanceRecordCommandHandler : BaseHandler, IRequestHandler<DeleteMaintenanceRecordCommand, Unit>
    {
        public DeleteMaintenanceRecordCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteMaintenanceRecordCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetReadRepository<MaintenanceRecord>();
            var entity = await repository.GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (entity == null)
                throw new KeyNotFoundException($"Maintenance record with ID {request.Id} not found or already deleted.");

            entity.IsDeleted = true;

           await _unitOfWork.GetWriteRepository<MaintenanceRecord>().UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
