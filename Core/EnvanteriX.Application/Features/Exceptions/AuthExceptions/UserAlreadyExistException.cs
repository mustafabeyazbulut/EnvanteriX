using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AuthExceptions
{
    public class UserAlreadyExistException : BaseException
    {
        public UserAlreadyExistException() : base("Böyle bir kullanıcı zaten var!") { }
    }
}
