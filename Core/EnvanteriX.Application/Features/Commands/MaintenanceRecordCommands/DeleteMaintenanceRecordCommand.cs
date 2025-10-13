using MediatR;

namespace EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands
{
    public class DeleteMaintenanceRecordCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public DeleteMaintenanceRecordCommand(int id)
        {
            Id = id;
        }
    }
}
