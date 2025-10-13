namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum UserEndpoint
    {
        GetAll,
        GetAllActive,
        GetById,
        GetByEmail,
        Create,
        Update,
        Delete,
        Active,
        DeActive,
        AddRole,
        RemoveRole,
        ChangePassword
    }

    public class UserApiUrl : BaseApiUrl
    {
        private const string BasePath = "user";

        public string GetUrl(UserEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                UserEndpoint.GetAll => $"{BasePath}/get-all",
                UserEndpoint.GetAllActive => $"{BasePath}/get-all-active",
                UserEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                UserEndpoint.GetByEmail => $"{BasePath}/get-by-email/",
                UserEndpoint.Create => $"{BasePath}/create",
                UserEndpoint.Update => $"{BasePath}/update",
                UserEndpoint.Delete => $"{BasePath}/delete/{id}",
                UserEndpoint.Active => $"{BasePath}/active/{id}",
                UserEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                UserEndpoint.AddRole => $"{BasePath}/add-role/{id}",
                UserEndpoint.RemoveRole => $"{BasePath}/remove-role/{id}",
                UserEndpoint.ChangePassword => $"{BasePath}/change-password",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
