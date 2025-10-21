using EnvanteriX.WebUI.Enums;
using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using EnvanteriX.WebUI.ViewModels.MaintenanceRecord;
using EnvanteriX.WebUI.ViewModels.Vendor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("MaintenanceRecord")]
    public class MaintenanceRecordController : BaseController
    {
        private readonly AssetApiUrl _AssetApiUrl;
        private readonly MaintenanceRecordApiUrl _maintenanceRecordApiUrl;
        private readonly VendorApiUrl _vendorApiUrl;

        public MaintenanceRecordController(IApiClientService apiClientService, ILogger<BaseController> logger, AssetApiUrl assetApiUrl, MaintenanceRecordApiUrl maintenanceRecordApiUrl, VendorApiUrl vendorApiUrl) : base(apiClientService, logger)
        {
            _AssetApiUrl = assetApiUrl;
            _maintenanceRecordApiUrl = maintenanceRecordApiUrl;
            _vendorApiUrl = vendorApiUrl;
        }

        private void PopulateVendors()
        {
            var Vendors = _apiClientService.GetAsync<List<VendorViewModel>>(_vendorApiUrl.GetUrl(VendorEndpoint.GetAllActive)).Result;
            ViewBag.Vendors = new SelectList(Vendors, "Id", "VendorName");
        }

        [HttpGet("SendToMaintenance/{assetId}")]
        public async Task<IActionResult> SendToMaintenance(int assetId)
        {
            try
            {
                var assetValue = await _apiClientService.GetAsync<UpdateAssetViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById, assetId));
                if (assetValue == null)
                {
                    throw new Exception("Varlık bulunamadı");
                }
                if (assetValue.Status == StatusEnum.KullanimDisi || assetValue.Status == StatusEnum.Tamirde)
                {
                    throw new Exception("Varlık durumu " + assetValue.Status + " olduğu için bakıma gönderilememektedir.");
                }
                PopulateVendors();
                return View(new CreateMaintenanceRecordViewModel
                {
                    AssetId= assetValue.Id,
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
        }
        [HttpPost("SendToMaintenance")]
        public async Task<IActionResult> SendToMaintenance(CreateMaintenanceRecordViewModel model)
        {
            try
            {
                var assetValue = await _apiClientService.GetAsync<UpdateAssetViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById, model.AssetId));
                if (assetValue == null)
                {
                    throw new Exception("Varlık bulunamadı");
                }
                if (assetValue.Status == StatusEnum.KullanimDisi || assetValue.Status == StatusEnum.Tamirde)
                {
                    throw new Exception("Varlık durumu " + assetValue.Status + " olduğu için bakıma gönderilememektedir.");
                }
                var result = await _apiClientService.PostAsync<object>(
                    _maintenanceRecordApiUrl.GetUrl(MaintenanceRecordEndpoint.Create), model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                PopulateVendors();
                return RedirectAfterPost(true, model);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("AssignAfterRepair/{assetId}")]
        public async Task<IActionResult> AssignAfterRepair(int assetId)
        {
            try
            {
                var assetValue = await _apiClientService.GetAsync<UpdateAssetViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById, assetId));
                if (assetValue == null)
                {
                    throw new Exception("Varlık bulunamadı");
                }
                if (assetValue.Status != StatusEnum.Tamirde)
                {
                    throw new Exception("Varlık durumu " + assetValue.Status + " olduğu için bakımdan geldi olarak işaretlenememektedir.");
                }
                PopulateVendors();
                var value=await _apiClientService.GetAsync<UpdateMaintenanceRecordViewModel>(_maintenanceRecordApiUrl.GetUrl(MaintenanceRecordEndpoint.GetLastOpenMaintenanceRecordByAssetId, assetId));
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
        }

        [HttpPost("AssignAfterRepair")]
        public async Task<IActionResult> AssignAfterRepair(UpdateMaintenanceRecordViewModel model)
        {
            try
            {
                var assetValue = await _apiClientService.GetAsync<UpdateAssetViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById, model.AssetId));
                if (assetValue == null)
                {
                    throw new Exception("Varlık bulunamadı");
                }
                if (assetValue.Status != StatusEnum.Tamirde)
                {
                    throw new Exception("Varlık durumu " + assetValue.Status + " olduğu için bakımdan geldi olarak işaretlenememektedir.");
                }
                var result = await _apiClientService.PutAsync<object>(
                    _maintenanceRecordApiUrl.GetUrl(MaintenanceRecordEndpoint.Update), model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                PopulateVendors();
                return RedirectAfterPost(true, model);
            }
            return RedirectAfterPost(false);
        }
    }
}
