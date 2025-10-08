namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum VendorEndpoint
    {
        GetAll,
        GetById,
        Create,
        Update,
        Delete,
        Active,
        DeActive
    }

    public class VendorApiUrl : BaseApiUrl
    {
        private const string BasePath = "vendor";

        public string GetUrl(VendorEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                VendorEndpoint.GetAll => $"{BasePath}/get-all",
                VendorEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                VendorEndpoint.Create => $"{BasePath}/create",
                VendorEndpoint.Update => $"{BasePath}/update",
                VendorEndpoint.Delete => $"{BasePath}/delete/{id}",
                VendorEndpoint.Active => $"{BasePath}/active/{id}",
                VendorEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
