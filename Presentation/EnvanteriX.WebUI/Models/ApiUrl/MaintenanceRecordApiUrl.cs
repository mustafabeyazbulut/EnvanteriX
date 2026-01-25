namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public enum MaintenanceRecordEndpoint
    {
        GetAll,
        GetAllPaginated,
        GetAllByAssetId,
        GetLastOpenMaintenanceRecordByAssetId,
        GetById,
        Create,
        Update,
        Delete,
        Active,
        DeActive
    }

    public class MaintenanceRecordApiUrl : BaseApiUrl
    {
        private const string BasePath = "maintenance-record";

        public string GetUrl(MaintenanceRecordEndpoint endpoint, int? id = null)
        {
            return endpoint switch
            {
                MaintenanceRecordEndpoint.GetAll => $"{BasePath}/get-all",
                MaintenanceRecordEndpoint.GetAllPaginated => $"{BasePath}/get-all-paginated",
                MaintenanceRecordEndpoint.GetAllByAssetId => $"{BasePath}/get-all-by-asset-id/{id}",
                MaintenanceRecordEndpoint.GetLastOpenMaintenanceRecordByAssetId => $"{BasePath}/get-last-open-maintenance-record-by-asset-id/{id}",
                MaintenanceRecordEndpoint.GetById => $"{BasePath}/get-by-id/{id}",
                MaintenanceRecordEndpoint.Create => $"{BasePath}/create",
                MaintenanceRecordEndpoint.Update => $"{BasePath}/update",
                MaintenanceRecordEndpoint.Delete => $"{BasePath}/delete/{id}",
                MaintenanceRecordEndpoint.Active => $"{BasePath}/active/{id}",
                MaintenanceRecordEndpoint.DeActive => $"{BasePath}/de-active/{id}",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }
}
