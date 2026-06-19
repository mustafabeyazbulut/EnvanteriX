using EnvanteriX.WebUI.Enums;

namespace EnvanteriX.WebUI.ViewModels.Asset
{
    public class CreateAssetViewModel
    {
        public string AssetTag { get; set; }
        public string SerialNumber { get; set; }
        public int AssetTypeId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int VendorId { get; set; }
        public bool IsRented { get; set; }
        public DateTime? RentalStartDate { get; set; }
        public int LocationId { get; set; }
        public int? AssignedUserId { get; set; }
        public int? AssignedDepartmentId { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Stokta;
    }
}
