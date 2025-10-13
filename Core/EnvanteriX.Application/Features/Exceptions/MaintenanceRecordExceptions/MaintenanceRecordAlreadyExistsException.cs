using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.MaintenanceRecordExceptions
{
    public class MaintenanceRecordAlreadyExistsException : BaseException
    {
       
        public MaintenanceRecordAlreadyExistsException(string asset)
            : base($"'{asset}' id'li varlığın kapanmamış bakım kaydı bulunuyor.")
        {
        }
    }
}
