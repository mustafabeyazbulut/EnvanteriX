namespace EnvanteriX.WebUI.ViewModels.AssetMovement
{
    public class CreateAssetMovementViewModel
    {
        public int AssetId { get; set; }
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }
        public int? FromLocationId { get; set; }
        public int? ToLocationId { get; set; }
        public string Note { get; set; }
    }
}
