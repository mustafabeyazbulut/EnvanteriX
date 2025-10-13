using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.MaintenanceRecord;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.MaintenanceRecordViewComponents
{
    public class _GetAllByAssetId_MaintenanceRecord_ComponentPartial:ViewComponent
    {
        private readonly IApiClientService _apiClientService;
        private readonly MaintenanceRecordApiUrl _maintenanceRecordApiUrl;
        public _GetAllByAssetId_MaintenanceRecord_ComponentPartial(MaintenanceRecordApiUrl maintenanceRecordApiUrl, IApiClientService apiClientService)
        {
            _maintenanceRecordApiUrl = maintenanceRecordApiUrl;
            _apiClientService = apiClientService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<MaintenanceRecordViewModel>>(_maintenanceRecordApiUrl.GetUrl(MaintenanceRecordEndpoint.GetAllByAssetId,id));
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<MaintenanceRecordViewModel>());
        }
    }
}
