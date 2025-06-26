using MediatR;
using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;
using System;

namespace EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands
{
    public class CreateMaintenanceRecordCommand : IRequest<CreateMaintenanceRecordCommandResult>
    {
        public int AssetId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string PerformedBy { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int VendorId { get; set; }
    }
}
