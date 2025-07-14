using System;

namespace EnvanteriX.Application.Features.Results.MaintenanceRecordResults
{
    public class GetAllMaintenanceRecordsQueryResult
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public DateTime StartDate { get; set; }      // Başlangıç Tarihi
        public DateTime? EndDate { get; set; }        // Bitiş Tarihi
        public string PerformedBy { get; set; }
        public string PreServiceDescription { get; set; }
        public string? PostServiceDescription { get; set; }
        public decimal Cost { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
    }
}
