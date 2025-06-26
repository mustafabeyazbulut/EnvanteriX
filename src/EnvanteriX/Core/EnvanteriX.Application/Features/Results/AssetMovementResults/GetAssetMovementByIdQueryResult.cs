namespace EnvanteriX.Application.Features.Results.AssetMovementResults
{
    public class GetAssetMovementByIdQueryResult
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string AssetCode { get; set; }
        public int? FromUserId { get; set; }
        public string FromUserFullName { get; set; }
        public int? ToUserId { get; set; }
        public string ToUserFullName { get; set; }
        public int? FromLocationId { get; set; }
        public string FromLocationName { get; set; }
        public int? ToLocationId { get; set; }
        public string ToLocationName { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
