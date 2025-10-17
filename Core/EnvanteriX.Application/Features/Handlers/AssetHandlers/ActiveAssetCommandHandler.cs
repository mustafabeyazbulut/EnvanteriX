using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetCommands;
using EnvanteriX.Application.Features.Rules.AssetRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.AssetHandlers
{
    public class ActiveAssetCommandHandler : BaseHandler, IRequestHandler<ActiveAssetCommand, Unit>
    {
        private readonly AssetRules _assetRules;
        public ActiveAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetRules assetRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetRules = assetRules;
        }

        public async Task<Unit> Handle(ActiveAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await _unitOfWork.GetReadRepository<Asset>().GetAsync(x => x.Id == request.Id);
            await _assetRules.AssetShouldExist(asset);
            asset.LastModifiedByEmail=_userEmail;
            asset.IsDeleted = false;
            await _unitOfWork.GetWriteRepository<Asset>().UpdateAsync(asset);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
