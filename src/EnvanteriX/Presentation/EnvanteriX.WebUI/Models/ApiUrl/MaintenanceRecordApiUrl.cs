namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum MaintenanceRecordEndpoint
    {
        GetAll,
        GetById,
        Create,
        Update,
        Delete,
        Active,
        DeActive
    }

    public class MaintenanceRecordApiUrl : BaseApiUrl
    {
        private const string BasePath = "maintenancerecord";

        public string GetUrl(MaintenanceRecordEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                MaintenanceRecordEndpoint.GetAll => $"{BasePath}/get-all",
                MaintenanceRecordEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                MaintenanceRecordEndpoint.Create => $"{BasePath}/create",
                MaintenanceRecordEndpoint.Update => $"{BasePath}/update",
                MaintenanceRecordEndpoint.Delete => $"{BasePath}/delete",
                MaintenanceRecordEndpoint.Active => $"{BasePath}/active/{id}",
                MaintenanceRecordEndpoint.DeActive => $"{BasePath}/deactive/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
