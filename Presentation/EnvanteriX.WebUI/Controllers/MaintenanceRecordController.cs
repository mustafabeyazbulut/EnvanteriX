using EnvanteriX.WebUI.Enums;
using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using EnvanteriX.WebUI.ViewModels.MaintenanceRecord;
using EnvanteriX.WebUI.ViewModels.Vendor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("MaintenanceRecord")]
    [Authorize(Roles = "admin")]

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

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var url = _maintenanceRecordApiUrl.GetUrl(MaintenanceRecordEndpoint.GetById, id);
                var result = await _apiClientService.GetAsync<MaintenanceRecordDetailViewModel>(url);
                
                if (result == null)
                {
                    TempData["ErrorMessage"] = "Bakım kaydı bulunamadı.";
                    return RedirectToAction("Index");
                }

                return View(result);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Detaylar yüklenirken hata oluştu: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        private async Task PopulateVendorsAsync()
        {
            var Vendors = await _apiClientService.GetAsync<List<VendorViewModel>>(_vendorApiUrl.GetUrl(VendorEndpoint.GetAllActive));
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
                await PopulateVendorsAsync();
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
                await PopulateVendorsAsync();
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
                await PopulateVendorsAsync();
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
                await PopulateVendorsAsync();
                return RedirectAfterPost(true, model);
            }
            return RedirectAfterPost(false);
        }
    }
}
