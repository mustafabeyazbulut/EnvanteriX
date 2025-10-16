namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum Portal365Endpoint
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

    public class Portal365ApiUrl : BaseApiUrl
    {
        private const string BasePath = "Portal365";

        public string GetUrl(Portal365Endpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                Portal365Endpoint.GetAll => $"{BasePath}/get-all",
                Portal365Endpoint.GetAllActive => $"{BasePath}/get-all-active",
                Portal365Endpoint.GetById => $"{BasePath}/get-by-id/{id}",
                Portal365Endpoint.Create => $"{BasePath}/create",
                Portal365Endpoint.Update => $"{BasePath}/update",
                Portal365Endpoint.Delete => $"{BasePath}/delete/{id}",
                Portal365Endpoint.Active => $"{BasePath}/active/{id}",
                Portal365Endpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
