using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.Portal365Exceptions
{
    public class Portal365AlreadyExistsException : BaseException
    {
        public Portal365AlreadyExistsException(string clientId)
            : base($"'{clientId}' adlı Portal365 entegrasyonu zaten mevcut.")
        {
        }
    }
}
