using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AssetExceptions
{
    public class AssetShouldNotHaveAnyMaintenanceRecordException : BaseException
    {
        public AssetShouldNotHaveAnyMaintenanceRecordException(string name)
            : base($"'{name}' adlı varlığa ait kayıtlı bakım kayıtları var. Önce bakım kayıtlarını silmelisiniz.")
        {
        }
    }
}
