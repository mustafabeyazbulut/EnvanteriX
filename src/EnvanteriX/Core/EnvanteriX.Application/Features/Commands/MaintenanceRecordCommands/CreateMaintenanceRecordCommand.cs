using MediatR;
using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;

namespace EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands
{
    public class CreateMaintenanceRecordCommand : IRequest<CreateMaintenanceRecordCommandResult>
    {
        public int AssetId { get; set; }
        public string PreServiceDescription { get; set; }
        public int VendorId { get; set; }
    }
}
