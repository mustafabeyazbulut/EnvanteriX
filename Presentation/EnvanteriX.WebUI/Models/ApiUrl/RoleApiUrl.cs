namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum RoleEndpoint
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

    public class RoleApiUrl : BaseApiUrl
    {
        private const string BasePath = "role";

        public string GetUrl(RoleEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                RoleEndpoint.GetAll => $"{BasePath}/get-all",
                RoleEndpoint.GetAllActive => $"{BasePath}/get-all-active",
                RoleEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                RoleEndpoint.Create => $"{BasePath}/create",
                RoleEndpoint.Update => $"{BasePath}/update",
                RoleEndpoint.Delete => $"{BasePath}/delete/{id}",
                RoleEndpoint.Active => $"{BasePath}/active/{id}",
                RoleEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
