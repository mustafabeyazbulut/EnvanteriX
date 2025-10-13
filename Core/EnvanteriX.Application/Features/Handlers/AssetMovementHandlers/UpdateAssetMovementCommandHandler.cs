using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetMovementCommands;
using EnvanteriX.Application.Features.Rules.AssetMovementRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetMovementHandlers
{
    public class UpdateAssetMovementCommandHandler : BaseHandler, IRequestHandler<UpdateAssetMovementCommand, Unit>
    {
        private readonly AssetMovementRules _assetMovementRules;
        public UpdateAssetMovementCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetMovementRules assetMovementRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetMovementRules = assetMovementRules;
        }

        public async Task<Unit> Handle(UpdateAssetMovementCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<AssetMovement>().GetAsync(x => x.Id == request.Id);
            await _assetMovementRules.AssetMovementShouldExist(model);

            model.AssetId = request.AssetId;
            model.FromUserId = request.FromUserId;
            model.ToUserId = request.ToUserId;
            model.FromLocationId = request.FromLocationId;
            model.ToLocationId = request.ToLocationId;
            model.Note = request.Note;

            await _unitOfWork.GetWriteRepository<AssetMovement>().UpdateAsync(model);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
