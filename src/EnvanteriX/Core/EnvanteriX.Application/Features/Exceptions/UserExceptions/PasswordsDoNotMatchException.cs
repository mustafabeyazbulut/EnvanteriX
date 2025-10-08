using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.UserExceptions
{
    public class PasswordsDoNotMatchException : BaseException
    {
        public PasswordsDoNotMatchException() : base("Parolalar uyuşmamaktadır.") { }
    }

}
