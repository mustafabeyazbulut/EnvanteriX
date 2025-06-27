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
        public Task RoleAlreadyExists(Role? model)
        {
            if (model is not null) throw new RoleAlreadyExistsException(model.Name);
            return Task.CompletedTask;
        }

        public async Task RoleShouldNotBeAssignedToUsersRule(IList<User> usersInRole,string name)
        {
            if (usersInRole.Any())
                throw new RoleAssignedToUsersException(name);
        }
    }
}
