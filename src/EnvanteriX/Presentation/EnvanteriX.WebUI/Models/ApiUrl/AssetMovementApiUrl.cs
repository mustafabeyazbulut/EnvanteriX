namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum AssetMovementEndpoint
    {
        GetAll,
        GetById,
        Create,
        Update,
        Delete,
        Active,
        DeActive
    }

    public class AssetMovementApiUrl : BaseApiUrl
    {
        private const string BasePath = "assetmovement";

        public string GetUrl(AssetMovementEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                AssetMovementEndpoint.GetAll => $"{BasePath}/get-all",
                AssetMovementEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                AssetMovementEndpoint.Create => $"{BasePath}/create",
                AssetMovementEndpoint.Update => $"{BasePath}/update",
                AssetMovementEndpoint.Delete => $"{BasePath}/delete",
                AssetMovementEndpoint.Active => $"{BasePath}/active/{id}",
                AssetMovementEndpoint.DeActive => $"{BasePath}/deactive/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
