using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.BrandExceptions
{
   
    public class BrandShouldNotHaveAnyModelException : BaseException
    {
        public BrandShouldNotHaveAnyModelException(string brandName)
            : base($"'{brandName}' adlı markaya ait kayıtlı modeller var. Önce modelleri silmelisiniz.")
        {
        }
    }
}
