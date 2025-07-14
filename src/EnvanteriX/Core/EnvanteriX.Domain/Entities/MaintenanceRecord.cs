using EnvanteriX.Domain.Common;

namespace EnvanteriX.Domain.Entities
{
    public class MaintenanceRecord : EntityBase, IEntityBase
    {
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public DateTime StartDate { get; set; }      // Başlangıç Tarihi
        public DateTime? EndDate { get; set; }        // Bitiş Tarihi
        public string? PerformedBy { get; set; }
        public string PreServiceDescription { get; set; }
        public string? PostServiceDescription { get; set; }
        public decimal? Cost { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
