using EnvanteriX.Domain.Common;

namespace EnvanteriX.Domain.Entities
{
    public class MaintenanceRecord : EntityBase, IEntityBase
    {
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string PerformedBy { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
