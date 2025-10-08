using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AuthExceptions
{
    public class PasswordsDoNotMatchException : BaseException
    {
        public PasswordsDoNotMatchException() : base("Parolalar uyuşmamaktadır.") { }
    }
}

