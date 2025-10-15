namespace EnvanteriX.WebUI.ViewModels.MaintenanceRecord
{
    public class UpdateMaintenanceRecordViewModel
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string PerformedBy { get; set; }
        public string PreServiceDescription { get; set; }
        public string PostServiceDescription { get; set; }
        public int VendorId { get; set; }
    }
}
