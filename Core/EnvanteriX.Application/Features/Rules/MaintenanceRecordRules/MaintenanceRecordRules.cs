using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.MaintenanceRecordExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.MaintenanceRecordRules
{
    public class MaintenanceRecordRules : BaseRules
    {
        public Task MaintenanceRecordShouldExist(MaintenanceRecord? model)
        {
            if (model is null) throw new MaintenanceRecordNotFoundException();
            return Task.CompletedTask;
        }
        public Task MaintenanceRecordAlreadyExists(MaintenanceRecord? model)
        {
            if (model is not null) throw new MaintenanceRecordAlreadyExistsException(model.AssetId.ToString());
            return Task.CompletedTask;
        }
    }
}
