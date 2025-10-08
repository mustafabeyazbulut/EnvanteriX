namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum AuthEndpoint
    {
        Register,
    }

    public class AuthApiUrl : BaseApiUrl
    {
        private const string BasePath = "auth";

        public string GetUrl(AuthEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                AuthEndpoint.Register => $"{BasePath}/register",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}

