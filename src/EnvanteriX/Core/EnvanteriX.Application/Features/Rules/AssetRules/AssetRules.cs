using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.AssetExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.AssetRules
{
    public class AssetRules : BaseRules
    {
        public Task AssetShouldExist(Asset? model)
        {
            if (model is null) throw new AssetNotFoundException();
            return Task.CompletedTask;
        }
        public Task AssetAlreadyExists(Asset? Asset)
        {
            if (Asset is not null) throw new AssetAlreadyExistsException(Asset.SerialNumber);
            return Task.CompletedTask;
        }
        public Task AssetAlreadyExists(bool AssetExists, string name)
        {
            if (AssetExists) throw new AssetAlreadyExistsException(name);
            return Task.CompletedTask;
        }
        public Task AssetShouldNotHaveAnyAssetMovement(bool hasAnyAssetMovement, string name)
        {
            if (hasAnyAssetMovement)
                throw new AssetShouldNotHaveAnyAssetMovementException(name);
            return Task.CompletedTask;
        }
        public Task AssetShouldNotHaveAnySoftwareLicense(bool hasAnyLicance, string name)
        {
            if (hasAnyLicance)
                throw new AssetShouldNotHaveAnySoftwareLicenseException(name);
            return Task.CompletedTask;
        }
        public Task AssetShouldNotHaveAnyMaintenanceRecord(bool hasAnyMaintenanceRecord, string name)
        {
            if (hasAnyMaintenanceRecord)
                throw new AssetShouldNotHaveAnyMaintenanceRecordException(name);
            return Task.CompletedTask;
        }
    }
}
