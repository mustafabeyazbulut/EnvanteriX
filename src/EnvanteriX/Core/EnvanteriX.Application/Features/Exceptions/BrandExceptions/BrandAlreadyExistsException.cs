using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.BrandExceptions
{
    public class BrandAlreadyExistsException : BaseException
    {
        public BrandAlreadyExistsException(string brandName)
            : base($"'{brandName}' adlı marka zaten mevcut.")
        {
        }
    }
}
