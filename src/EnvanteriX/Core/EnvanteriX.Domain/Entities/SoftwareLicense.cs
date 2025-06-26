using EnvanteriX.Domain.Common;

namespace EnvanteriX.Domain.Entities
{
    public class SoftwareLicense : EntityBase, IEntityBase
    {
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public string LicenseKey { get; set; }
        public string SoftwareName { get; set; }
        public string Version { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? AssignedUserId { get; set; }
        public User AssignedUser { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
