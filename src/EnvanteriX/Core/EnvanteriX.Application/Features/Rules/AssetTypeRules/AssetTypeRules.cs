using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.AssetTypeExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.AssetTypeRules
{
  public  class AssetTypeRules : BaseRules
    {
        public Task AssetTypeAlreadyExists(AssetType? AssetType)
        {
            if (AssetType is not null) throw new AssetTypeAlreadyExistsException(AssetType.TypeName);
            return Task.CompletedTask;
        }
        public Task AssetTypeAlreadyExists(bool AssetTypeExists, string name)
        {
            if (AssetTypeExists) throw new AssetTypeAlreadyExistsException(name);
            return Task.CompletedTask;
        }
        public Task AssetTypeShouldExist(AssetType? model)
        {
            if (model is null) throw new AssetTypeNotFoundException();
            return Task.CompletedTask;
        }
        public Task AssetTypeShouldExist(bool AssetTypeExists)
        {
            if (!AssetTypeExists) throw new AssetTypeNotFoundException();
            return Task.CompletedTask;
        }
        public Task AssetTypeShouldNotHaveAnyAsset(bool hasAnyAssetType, string name)
        {
            if (hasAnyAssetType)
                throw new AssetTypeShouldNotHaveAnyAssetException(name);
            return Task.CompletedTask;
        }
    }
}
