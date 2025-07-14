using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.MaintenanceRecordExceptions
{
    public class MaintenanceRecordNotFoundException : BaseException
    {
        public MaintenanceRecordNotFoundException() : base("Bakım Kaydı bulunamadı.") { }
    }
}
