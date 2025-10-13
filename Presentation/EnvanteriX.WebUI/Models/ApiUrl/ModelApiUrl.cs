namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum ModelEndpoint
    {
        GetAll,
        GetAllActive,
        GetAllActiveByBrandId,
        GetById,
        Create,
        Update,
        Delete,
        Active,
        DeActive
    }

    public class ModelApiUrl : BaseApiUrl
    {
        private const string BasePath = "model";

        public string GetUrl(ModelEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                ModelEndpoint.GetAll => $"{BasePath}/get-all",
                ModelEndpoint.GetAllActive => $"{BasePath}/get-all-active",
                ModelEndpoint.GetAllActiveByBrandId => $"{BasePath}/get-all-active-by-brand-id/{id}",
                ModelEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                ModelEndpoint.Create => $"{BasePath}/create",
                ModelEndpoint.Update => $"{BasePath}/update",
                ModelEndpoint.Delete => $"{BasePath}/delete/{id}",
                ModelEndpoint.Active => $"{BasePath}/active/{id}",
                ModelEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
