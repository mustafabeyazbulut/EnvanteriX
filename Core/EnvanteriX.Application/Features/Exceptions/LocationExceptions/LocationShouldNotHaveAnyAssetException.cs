using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.LocationExceptions
{
    public class LocationShouldNotHaveAnyAssetException : BaseException
    {
        public LocationShouldNotHaveAnyAssetException(string name)
            : base($"'{name}' adlı lokasyona ait kayıtlı varlıklar var. Önce varlıkların lokasyonlarını değiştirmelisiniz.")
        {
        }
    }
}
