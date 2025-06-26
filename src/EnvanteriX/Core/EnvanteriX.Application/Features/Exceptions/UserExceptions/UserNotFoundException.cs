using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.UserExceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException() : base("Kullanıcı bulunamadı.") { }
    }
}
