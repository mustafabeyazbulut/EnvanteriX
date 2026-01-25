namespace EnvanteriX.WebUI.ViewModels.MaintenanceRecord
{
    public class PaginatedMaintenanceRecordViewModel
    {
        public List<MaintenanceRecordListItemViewModel> Items { get; set; } = new List<MaintenanceRecordListItemViewModel>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        
        // UI Dropdowns
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Vendors { get; set; } = new();
    }

    public class MaintenanceRecordListItemViewModel
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string? AssetTag { get; set; }
        public string AssetName { get; set; }
        public string AssetTypeName { get; set; }
        public string LocationName { get; set; } // Eklenen alan
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DurationDays { get; set; }
        public bool IsCompleted { get; set; }
        public string? PerformedBy { get; set; }
        public string PreServiceDescription { get; set; }
        public string? PostServiceDescription { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
    }
}
