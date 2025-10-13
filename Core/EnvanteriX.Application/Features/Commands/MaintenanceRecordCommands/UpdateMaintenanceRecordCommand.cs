using MediatR;

namespace EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands
{
    public class UpdateMaintenanceRecordCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string PerformedBy { get; set; }
        public string PreServiceDescription { get; set; }
        public string PostServiceDescription { get; set; }
        public decimal Cost { get; set; }
        public int VendorId { get; set; }
    }
}
