using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.VendorExceptions
{
    public class VendorShouldNotHaveAnySoftwareLicenseException : BaseException
    {
        public VendorShouldNotHaveAnySoftwareLicenseException(string name)
            : base($"'{name}' adlı tedarikçiye ait kayıtlı lisanslar var. Önce lisansların tedarikçilerini değiştirmelisiniz.")
        {
        }
    }
}
