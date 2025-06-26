using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetMovementCommands;
using EnvanteriX.Application.Features.Results.AssetMovementResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetMovementHandlers
{
    public class UpdateAssetMovementCommandHandler : BaseHandler, IRequestHandler<UpdateAssetMovementCommand, UpdateAssetMovementCommandResult>
    {
        public UpdateAssetMovementCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<UpdateAssetMovementCommandResult> Handle(UpdateAssetMovementCommand request, CancellationToken cancellationToken)
        {
            var repoRead = _unitOfWork.GetReadRepository<AssetMovement>();
            var entity = await repoRead.GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (entity == null)
                throw new KeyNotFoundException($"AssetMovement with Id {request.Id} not found.");

            var asset = await _unitOfWork.GetReadRepository<Asset>()
                .GetAsync(x => x.Id == request.AssetId && !x.IsDeleted);
            if (asset == null)
                throw new KeyNotFoundException($"Asset with Id {request.AssetId} not found.");

            entity.AssetId = request.AssetId;
            entity.FromUserId = request.FromUserId;
            entity.ToUserId = request.ToUserId;
            entity.FromLocationId = request.FromLocationId;
            entity.ToLocationId = request.ToLocationId;
            entity.Note = request.Note;

            await _unitOfWork.GetWriteRepository<AssetMovement>().UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UpdateAssetMovementCommandResult>(entity);
        }
    }
}
