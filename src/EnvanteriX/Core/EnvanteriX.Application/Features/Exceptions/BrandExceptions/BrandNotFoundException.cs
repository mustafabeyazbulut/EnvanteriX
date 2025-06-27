using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.BrandExceptions
{
    public class BrandNotFoundException : BaseException
    {
        public BrandNotFoundException() : base("Marka bulunamadı.") { }
    }
}
