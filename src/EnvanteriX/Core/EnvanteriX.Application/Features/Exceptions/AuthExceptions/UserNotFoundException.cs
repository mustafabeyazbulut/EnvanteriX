using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AuthExceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException() : base("Kullanıcı bulunamadı.") { }
    }
}
