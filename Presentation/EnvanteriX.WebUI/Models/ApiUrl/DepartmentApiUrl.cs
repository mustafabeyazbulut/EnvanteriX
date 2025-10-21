namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum DepartmentEndpoint
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

    public class DepartmentApiUrl : BaseApiUrl
    {
        private const string BasePath = "Department";

        public string GetUrl(DepartmentEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                DepartmentEndpoint.GetAll => $"{BasePath}/get-all",
                DepartmentEndpoint.GetAllActive => $"{BasePath}/get-all-active",
                DepartmentEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                DepartmentEndpoint.Create => $"{BasePath}/create",
                DepartmentEndpoint.Update => $"{BasePath}/update",
                DepartmentEndpoint.Delete => $"{BasePath}/delete/{id}",
                DepartmentEndpoint.Active => $"{BasePath}/active/{id}",
                DepartmentEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
