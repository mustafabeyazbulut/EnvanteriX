﻿using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.UserExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.UserRules
{
    public class UserRules : BaseRules
    {
        public Task UserShouldExist(User? user)
        {
            if (user is null) throw new UserNotFoundException();
            return Task.CompletedTask;
        }
        public Task UserAlreadyExists(User? model)
        {
            if (model is not null) throw new UserAlreadyExistsException(model.UserName);
            return Task.CompletedTask;
        }
        public Task UserHasAssetMovements(bool hasMovements)
        {
            if (hasMovements) throw new UserHasAssetMovementsException();
            return Task.CompletedTask;
        }
        public Task UserDoesNotHaveRoleException(bool isInRole,string roleName)
        {
            if (!isInRole) throw new UserDoesNotHaveRoleException(roleName);
            return Task.CompletedTask;
        }
    }
}
