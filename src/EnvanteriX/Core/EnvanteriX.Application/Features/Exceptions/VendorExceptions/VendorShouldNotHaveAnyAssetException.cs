using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.VendorExceptions
{
    public class VendorShouldNotHaveAnyAssetException : BaseException
    {
        public VendorShouldNotHaveAnyAssetException(string name)
            : base($"'{name}' adlı tedarikçiye ait kayıtlı varlıklar var. Önce varlıkların tedarikçilerini değiştirmelisiniz.")
        {
        }
    }
}
