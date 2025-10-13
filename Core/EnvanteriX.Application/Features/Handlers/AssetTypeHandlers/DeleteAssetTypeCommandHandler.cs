using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetTypeCommands;
using EnvanteriX.Application.Features.Rules.AssetTypeRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetTypeHandlers
{
    public class DeleteAssetTypeCommandHandler :BaseHandler, IRequestHandler<DeleteAssetTypeCommand, Unit>
    {
        private readonly AssetTypeRules _assetTypeRules;
        public DeleteAssetTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetTypeRules assetTypeRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetTypeRules = assetTypeRules;
        }
        public async Task<Unit> Handle(DeleteAssetTypeCommand request, CancellationToken cancellationToken)
        {
            var assetType = await _unitOfWork.GetReadRepository<AssetType>()
                                                .GetAsync(x => x.Id == request.Id );
            await _assetTypeRules.AssetTypeShouldExist(assetType);
            var hasAnyAsset = await _unitOfWork.GetReadRepository<Asset>()
                                               .AnyAsync(x => x.AssetTypeId == request.Id && !x.IsDeleted);
            await _assetTypeRules.AssetTypeShouldNotHaveAnyAsset(hasAnyAsset, assetType.TypeName);
            await _unitOfWork.GetWriteRepository<AssetType>().HardDeleteAsync(assetType);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
