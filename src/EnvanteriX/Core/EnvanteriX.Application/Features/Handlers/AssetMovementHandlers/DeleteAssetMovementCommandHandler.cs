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
    public class DeleteAssetMovementCommandHandler : BaseHandler, IRequestHandler<DeleteAssetMovementCommand, Unit>
    {
        private readonly AssetMovementRules _assetMovementRules;
        public DeleteAssetMovementCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetMovementRules assetMovementRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetMovementRules = assetMovementRules;
        }
        public async Task<Unit> Handle(DeleteAssetMovementCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<AssetMovement>().GetAsync(x => x.Id == request.Id);
            await _assetMovementRules.AssetMovementShouldExist(model);
            await _unitOfWork.GetWriteRepository<AssetMovement>().HardDeleteAsync(model);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
