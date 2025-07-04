using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.VendorExceptions
{
    public class VendorShouldNotHaveAnyMaintenanceRecordException : BaseException
    {
        public VendorShouldNotHaveAnyMaintenanceRecordException(string name)
            : base($"'{name}' adlı tedarikçiye ait kayıtlı bakım kayıtları var. Önce bakım kayıtlarını silmelisiniz.")
        {
        }
    }
}
