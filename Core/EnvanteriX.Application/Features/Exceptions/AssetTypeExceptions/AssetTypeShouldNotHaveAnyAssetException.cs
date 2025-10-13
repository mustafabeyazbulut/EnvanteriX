using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AssetTypeExceptions
{
    public class AssetTypeShouldNotHaveAnyAssetException : BaseException
    {
        public AssetTypeShouldNotHaveAnyAssetException(string name)
            : base($"'{name}' adlı Varlık Tipine ait kayıtlı varlıklar var. Önce varlıkların Varlık Tiplerini değiştirmelisiniz.")
        {
        }
    }
}
