using EnvanteriX.Domain.Common;
using EnvanteriX.Domain.Enums;

namespace EnvanteriX.Domain.Entities
{
    // +
    public class Asset : EntityBase, IEntityBase
    {
        public string AssetTag { get; set; }
        public string SerialNumber { get; set; }
        public int AssetTypeId { get; set; }
        public AssetType AssetType { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime WarrantyEndDate { get; set; }
        public decimal Cost { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int? AssignedUserId { get; set; }
        public User AssignedUser { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }

        public ICollection<SoftwareLicense> SoftwareLicenses { get; set; }
        public ICollection<AssetMovement> AssetMovements { get; set; }
        public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
    }

}
