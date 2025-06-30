namespace EnvanteriX.Application.Features.Results.SoftwareLicenseResults
{
    public class GetAllSoftwareLicensesQueryResult
    {
        public int SoftwareLicenseId { get; set; }
        public int AssetId { get; set; }
        public string LicenseKey { get; set; }
        public string SoftwareName { get; set; }
        public string Version { get; set; }
        public int VendorId { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public int? AssignedUserId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
