using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.Portal365Exceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.Portal365Rules
{
    public class Portal365Rules : BaseRules
    {
        public Task Portal365ShouldExist(Portal365? model)
        {
            if (model is null) throw new Portal365NotFoundException();
            return Task.CompletedTask;
        }
        public Task Portal365AlreadyExists(Portal365? model)
        {
            if (model is not null) throw new Portal365AlreadyExistsException(model.ClientId);
            return Task.CompletedTask;
        }
        public Task Portal365AlreadyExists(bool Portal365Exists, string name)
        {
            if (Portal365Exists) throw new Portal365AlreadyExistsException(name);
            return Task.CompletedTask;
        }
        public Task AccessTokenMustExist(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new InvalidAccessTokenException();

            return Task.CompletedTask;
        }

    }
}
