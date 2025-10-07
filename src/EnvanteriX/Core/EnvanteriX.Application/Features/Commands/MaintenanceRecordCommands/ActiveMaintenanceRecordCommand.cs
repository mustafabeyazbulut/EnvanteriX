using MediatR;

namespace EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands
{
    public class ActiveMaintenanceRecordCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
