namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum AssetTypeEndpoint
    {
        GetAll,
        GetAllActive,
        GetById,
        Create,
        Update,
        Delete,
        Active,
        DeActive
    }

    public class AssetTypeApiUrl : BaseApiUrl
    {
        private const string BasePath = "asset-type";

        public string GetUrl(AssetTypeEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                AssetTypeEndpoint.GetAll => $"{BasePath}/get-all",
                AssetTypeEndpoint.GetAllActive => $"{BasePath}/get-all-active",
                AssetTypeEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                AssetTypeEndpoint.Create => $"{BasePath}/create",
                AssetTypeEndpoint.Update => $"{BasePath}/update",
                AssetTypeEndpoint.Delete => $"{BasePath}/delete/{id}",
                AssetTypeEndpoint.Active => $"{BasePath}/active/{id}",
                AssetTypeEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
