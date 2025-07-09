using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Results.AssetResults
{
   public class GetAssetByIdQueryResult
    {
        public int Id { get; set; }
        public string AssetTag { get; set; }
        public string SerialNumber { get; set; }
        public int AssetTypeId { get; set; }
        public string TypeName { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime WarrantyEndDate { get; set; }
        public decimal Cost { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
