using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.RoleExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.RoleRules
{
    public class RoleRules : BaseRules
    {
        public Task RoleShouldExistRule(Role? role)
        {
            if (role is null)
                throw new RoleShouldExistException();
            return Task.CompletedTask;
        }
    }
}
