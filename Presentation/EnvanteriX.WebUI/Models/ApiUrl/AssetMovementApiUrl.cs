namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum AssetMovementEndpoint
    {
        GetAll,
        GetAllByAssetId,
        GetById,
        Create,
        Update,
        Delete,
        Active,
        DeActive
    }

    public class AssetMovementApiUrl : BaseApiUrl
    {
        private const string BasePath = "asset-movement";

        public string GetUrl(AssetMovementEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                AssetMovementEndpoint.GetAll => $"{BasePath}/get-all",
                AssetMovementEndpoint.GetAllByAssetId => $"{BasePath}/get-all-by-asset-id/{id}",
                AssetMovementEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                AssetMovementEndpoint.Create => $"{BasePath}/create",
                AssetMovementEndpoint.Update => $"{BasePath}/update",
                AssetMovementEndpoint.Delete => $"{BasePath}/delete/{id}",
                AssetMovementEndpoint.Active => $"{BasePath}/active/{id}",
                AssetMovementEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
