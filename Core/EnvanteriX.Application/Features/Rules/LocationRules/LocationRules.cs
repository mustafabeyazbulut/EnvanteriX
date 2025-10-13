using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.LocationExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.LocationRules
{
   public class LocationRules : BaseRules
    {
        public Task LocationShouldExist(Location? model)
        {
            if (model is null) throw new LocationNotFoundException();
            return Task.CompletedTask;
        }
        public Task LocationAlreadyExists(Location? Location)
        {
            if (Location is not null) throw new LocationAlreadyExistsException(Location.Building);
            return Task.CompletedTask;
        }
        public Task LocationAlreadyExists(bool locationExists, string name)
        {
            if (locationExists) throw new LocationAlreadyExistsException(name);
            return Task.CompletedTask;
        }
        public Task LocationShouldNotHaveAnyAsset(bool hasAnyLocation, string name)
        {
            if (hasAnyLocation)
                throw new LocationShouldNotHaveAnyAssetException(name);
            return Task.CompletedTask;
        }
    }
}
