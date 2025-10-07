namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum AssetEndpoint
    {
        GetAll,
        GetById,
        Create,
        Update,
        Delete,
        Active,
        DeActive
    }

    public class AssetApiUrl : BaseApiUrl
    {
        private const string BasePath = "asset";

        public string GetUrl(AssetEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                AssetEndpoint.GetAll => $"{BasePath}/get-all",
                AssetEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                AssetEndpoint.Create => $"{BasePath}/create",
                AssetEndpoint.Update => $"{BasePath}/update",
                AssetEndpoint.Delete => $"{BasePath}/delete",
                AssetEndpoint.Active => $"{BasePath}/active/{id}",
                AssetEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
