using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AssetExceptions
{
    public class AssetShouldNotHaveAnyAssetMovementException : BaseException
    {
        public AssetShouldNotHaveAnyAssetMovementException(string name)
            : base($"'{name}' adlı varlığa ait kayıtlı hareketler var. Önce varlığın hareketlerini silmelisiniz.")
        {
        }
    }
}
