namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum AssetTypeEndpoint
    {
        GetAll,
        GetById,
        Create,
        Update,
        Delete
    }

    public class AssetTypeApiUrl : BaseApiUrl
    {
        private const string BasePath = "assettype";

        public string GetUrl(AssetTypeEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                AssetTypeEndpoint.GetAll => $"{BasePath}/get-all",
                AssetTypeEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                AssetTypeEndpoint.Create => $"{BasePath}/create",
                AssetTypeEndpoint.Update => $"{BasePath}/update",
                AssetTypeEndpoint.Delete => $"{BasePath}/delete",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
