namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum ModelEndpoint
    {
        GetAll,
        GetById,
        Create,
        Update,
        Delete
    }

    public class ModelApiUrl : BaseApiUrl
    {
        private const string BasePath = "model";

        public string GetUrl(ModelEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                ModelEndpoint.GetAll => $"{BasePath}/get-all",
                ModelEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                ModelEndpoint.Create => $"{BasePath}/create",
                ModelEndpoint.Update => $"{BasePath}/update",
                ModelEndpoint.Delete => $"{BasePath}/delete",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
