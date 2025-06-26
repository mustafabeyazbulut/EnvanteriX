using EnvanteriX.Application.Bases;
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
       
    }
}
