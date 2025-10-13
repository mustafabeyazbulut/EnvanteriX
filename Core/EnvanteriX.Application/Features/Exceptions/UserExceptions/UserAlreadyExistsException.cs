using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.UserExceptions
{
    public class UserAlreadyExistsException : BaseException
    {
        public UserAlreadyExistsException(string Name)
           : base($"'{Name}' adlı Kullanıcı zaten mevcut.")
        {
        }
    }
}
