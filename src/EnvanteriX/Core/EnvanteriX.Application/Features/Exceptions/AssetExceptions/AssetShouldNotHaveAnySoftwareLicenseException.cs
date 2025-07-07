using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AssetExceptions
{
    public class AssetShouldNotHaveAnySoftwareLicenseException : BaseException
    {
        public AssetShouldNotHaveAnySoftwareLicenseException(string name)
            : base($"'{name}' adlı varlığa ait kayıtlı lisanslar var. Önce lisansların varlıklarını değiştirmelisiniz.")
        {
        }
    }
}
