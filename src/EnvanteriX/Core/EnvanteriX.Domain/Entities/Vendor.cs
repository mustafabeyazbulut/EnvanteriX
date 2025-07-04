using EnvanteriX.Domain.Common;

namespace EnvanteriX.Domain.Entities
{
    // +
    public class Vendor : EntityBase, IEntityBase
    {
        public string VendorName { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Asset> Assets { get; set; }
        public ICollection<SoftwareLicense> Licenses { get; set; }
        public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
    }
}
