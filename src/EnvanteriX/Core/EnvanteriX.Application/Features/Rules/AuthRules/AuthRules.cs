using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.AuthExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.AuthRules
{
    public class AuthRules : BaseRules
    {
        public Task UserShouldNotBeExist(User? user)
        {
            if (user is not null) throw new UserAlreadyExistException();
            return Task.CompletedTask;
        }
        public Task UserShouldExist(User? user)
        {
            if (user is null) throw new UserNotFoundException();
            return Task.CompletedTask;
        }
        public Task EmailOrPasswordShouldNotBeInvalid(User? user, bool checkPassword)
        {
            if (user is null || !checkPassword) throw new EmailOrPasswordShouldNotBeInvalidException();
            return Task.CompletedTask;
        }
        public Task RefreshTokenShouldNotBeExpired(DateTime? expiryDate)
        {
            if (expiryDate <= DateTime.Now) throw new RefreshTokenShouldNotBeExpiredException();
            return Task.CompletedTask;
        }

        public Task EmailAddressShouldBeValid(User? user)
        {
            if (user is null) throw new EmailAddressShouldBeValidException();
            return Task.CompletedTask;
        }
        public Task UserShouldBeActive(User user)
        {
            if (!user.IsActive) // varsayalım IdentityUser'da IsActive bool özelliği var
                throw new UserInactiveException();
            return Task.CompletedTask;
        }
    }
}
