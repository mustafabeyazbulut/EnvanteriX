using MediatR;

namespace EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands
{
    public class DeActiveMaintenanceRecordCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
