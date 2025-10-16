using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.Portal365Exceptions
{
    public class Portal365NotFoundException : BaseException
    {
        public Portal365NotFoundException() : base("Portal 365 Entegrasyonu Bulunamadı.") { }
    }
}
