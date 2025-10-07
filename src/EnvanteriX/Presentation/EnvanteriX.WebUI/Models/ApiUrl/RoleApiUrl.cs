namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum RoleEndpoint
    {
        GetAll,
        GetById,
        Create,
        Update,
        Delete
    }

    public class RoleApiUrl : BaseApiUrl
    {
        private const string BasePath = "role";

        public string GetUrl(RoleEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                RoleEndpoint.GetAll => $"{BasePath}/get-all",
                RoleEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                RoleEndpoint.Create => $"{BasePath}/create",
                RoleEndpoint.Update => $"{BasePath}/update",
                RoleEndpoint.Delete => $"{BasePath}/delete",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
