namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum UserEndpoint
    {
        GetAll,
        GetById,
        Create,
        Update,
        Delete,
        Active,
        DeActive
    }

    public class UserApiUrl : BaseApiUrl
    {
        private const string BasePath = "user";

        public string GetUrl(UserEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                UserEndpoint.GetAll => $"{BasePath}/get-all",
                UserEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                UserEndpoint.Create => $"{BasePath}/create",
                UserEndpoint.Update => $"{BasePath}/update",
                UserEndpoint.Delete => $"{BasePath}/delete",
                UserEndpoint.Active => $"{BasePath}/active/{id}",
                UserEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
