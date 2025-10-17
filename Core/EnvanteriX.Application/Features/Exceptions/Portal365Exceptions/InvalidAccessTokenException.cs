using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.Portal365Exceptions
{
    public class InvalidAccessTokenException : BaseException
    {
        public InvalidAccessTokenException() : base("Acces Token oluşturulamadı. Bağlantı başarısız") { }
    }
}
