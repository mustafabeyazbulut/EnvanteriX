using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.AssetMovementExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.AssetMovementRules
{
  public  class AssetMovementRules : BaseRules
    {
        public Task AssetMovementShouldExist(AssetMovement? model)
        {
            if (model is null) throw new AssetMovementNotFoundException();
            return Task.CompletedTask;
        }
        public Task AssetMovementAlreadyExists(AssetMovement? model)
        {
            if (model is not null) throw new AssetMovementAlreadyExistsException();
            return Task.CompletedTask;
        }
    }
}
