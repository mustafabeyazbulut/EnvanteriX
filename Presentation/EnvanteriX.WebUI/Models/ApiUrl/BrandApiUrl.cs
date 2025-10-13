namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum BrandEndpoint
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

    public class BrandApiUrl : BaseApiUrl
    {
        private const string BasePath = "brand";

        public string GetUrl(BrandEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                BrandEndpoint.GetAll => $"{BasePath}/get-all",
                BrandEndpoint.GetAllActive => $"{BasePath}/get-all-active",
                BrandEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                BrandEndpoint.Create => $"{BasePath}/create",
                BrandEndpoint.Update => $"{BasePath}/update",
                BrandEndpoint.Delete => $"{BasePath}/delete/{id}",
                BrandEndpoint.Active => $"{BasePath}/active/{id}",
                BrandEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
