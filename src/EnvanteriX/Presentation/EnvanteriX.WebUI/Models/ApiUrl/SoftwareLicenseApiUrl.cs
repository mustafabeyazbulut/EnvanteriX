namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum SoftwareLicenseEndpoint
    {
        GetAll,
        GetById,
        Create,
        Update,
        Delete
    }

    public class SoftwareLicenseApiUrl : BaseApiUrl
    {
        private const string BasePath = "softwarelicense";

        public string GetUrl(SoftwareLicenseEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                SoftwareLicenseEndpoint.GetAll => $"{BasePath}/get-all",
                SoftwareLicenseEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                SoftwareLicenseEndpoint.Create => $"{BasePath}/create",
                SoftwareLicenseEndpoint.Update => $"{BasePath}/update",
                SoftwareLicenseEndpoint.Delete => $"{BasePath}/delete",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
