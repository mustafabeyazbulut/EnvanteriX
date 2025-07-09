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
    public class DeleteAssetCommandHandler : BaseHandler, IRequestHandler<DeleteAssetCommand, Unit>
    {
        private readonly AssetRules _assetRules;
        public DeleteAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetRules assetRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetRules = assetRules;
        }

        public async Task<Unit> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await _unitOfWork.GetReadRepository<Asset>().GetAsync(x => x.Id == request.Id);
            await _assetRules.AssetShouldExist(asset);

            var hasAnyAssetMovement = await _unitOfWork.GetReadRepository<AssetMovement>().AnyAsync(x => x.AssetId == request.Id);
            await _assetRules.AssetShouldNotHaveAnyAssetMovement(hasAnyAssetMovement, asset.SerialNumber);

            var hasAnySoftwareLicense = await _unitOfWork.GetReadRepository<SoftwareLicense>().AnyAsync(x => x.AssetId == request.Id);
            await _assetRules.AssetShouldNotHaveAnySoftwareLicense(hasAnySoftwareLicense, asset.SerialNumber);

            var hasAnyMaintenanceRecord = await _unitOfWork.GetReadRepository<MaintenanceRecord>().AnyAsync(x => x.AssetId == request.Id);
            await _assetRules.AssetShouldNotHaveAnyMaintenanceRecord(hasAnyMaintenanceRecord, asset.SerialNumber);

            await _unitOfWork.GetWriteRepository<Asset>().HardDeleteAsync(asset);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
