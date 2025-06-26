using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Results.AssetResults
{
    public class GetAssetsByTypeQueryResult
    {
        public int AssetId { get; set; }
        public string AssetTag { get; set; }
        public string SerialNumber { get; set; }
        public string TypeName { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public string VendorName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime WarrantyEndDate { get; set; }
        public decimal Cost { get; set; }
        public string LocationName { get; set; }
        public string AssignedUserName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public GetAssetsByTypeQueryResult(Asset asset)
        {
            AssetId = asset.Id;
            AssetTag = asset.AssetTag;
            SerialNumber = asset.SerialNumber;
            TypeName = asset.AssetType?.TypeName;
            ModelName = asset.Model?.ModelName;
            BrandName = asset.Model?.Brand?.BrandName;
            VendorName = asset.Vendor?.VendorName;
            PurchaseDate = asset.PurchaseDate;
            WarrantyEndDate = asset.WarrantyEndDate;
            Cost = asset.Cost;
            LocationName = $"{asset.Location?.Building} {asset.Location?.Room}";
            AssignedUserName = asset.AssignedUser?.FullName;
            Description = asset.Description;
            Status = asset.Status;
        }
    }
}
