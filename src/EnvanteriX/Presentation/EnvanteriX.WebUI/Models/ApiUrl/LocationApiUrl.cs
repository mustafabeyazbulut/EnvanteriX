namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum LocationEndpoint
    {
        GetAll,
        GetById,
        Create,
        Update,
        Delete,
        Active,
        DeActive
    }

    public class LocationApiUrl : BaseApiUrl
    {
        private const string BasePath = "location";

        public string GetUrl(LocationEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                LocationEndpoint.GetAll => $"{BasePath}/get-all",
                LocationEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                LocationEndpoint.Create => $"{BasePath}/create",
                LocationEndpoint.Update => $"{BasePath}/update",
                LocationEndpoint.Delete => $"{BasePath}/delete",
                LocationEndpoint.Active => $"{BasePath}/active/{id}",
                LocationEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
