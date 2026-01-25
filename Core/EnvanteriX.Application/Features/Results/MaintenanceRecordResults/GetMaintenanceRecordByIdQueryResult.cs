namespace EnvanteriX.Application.Features.Results.MaintenanceRecordResults
{
    public class GetMaintenanceRecordByIdQueryResult
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string? AssetTag { get; set; }
        public string AssetName { get; set; }
        public string SerialNumber { get; set; }
        public string AssetType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string? LocationName { get; set; } // Eklenen alan
        public string? AssetStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DurationDays { get; set; }
        public bool IsCompleted { get; set; }
        public string? PerformedBy { get; set; }
        public string PreServiceDescription { get; set; }
        public string? PostServiceDescription { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string? VendorPhone { get; set; }
        public string? VendorEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
