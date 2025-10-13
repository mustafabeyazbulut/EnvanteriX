using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.UserExceptions
{
    public class UserDoesNotHaveRoleException : BaseException
    {
        public UserDoesNotHaveRoleException(string roleName)
            : base($"Kullanıcı zaten '{roleName}' rolüne sahip değil.") { }
    }

}
