using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetMovementCommands;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetMovementHandlers
{
    public class DeleteAssetMovementCommandHandler : BaseHandler, IRequestHandler<DeleteAssetMovementCommand, Unit>
    {
        public DeleteAssetMovementCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteAssetMovementCommand request, CancellationToken cancellationToken)
        {
            var repoRead = _unitOfWork.GetReadRepository<AssetMovement>();
            var entity = await repoRead.GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (entity == null)
                throw new KeyNotFoundException($"AssetMovement with ID {request.Id} not found.");

            await _unitOfWork.GetWriteRepository<AssetMovement>().HardDeleteAsync(entity);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
