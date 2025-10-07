namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum BrandEndpoint
    {
        GetAll,
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
                BrandEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                BrandEndpoint.Create => $"{BasePath}/create",
                BrandEndpoint.Update => $"{BasePath}/update",
                BrandEndpoint.Delete => $"{BasePath}/delete",
                BrandEndpoint.Active => $"{BasePath}/active",
                BrandEndpoint.DeActive => $"{BasePath}/deactive",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
