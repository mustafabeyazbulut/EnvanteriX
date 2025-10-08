using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Vendor;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("Vendor")]
    public class VendorController : BaseController
    {
        private readonly VendorApiUrl _vendorApiUrl;
        public VendorController(IApiClientService apiClientService, VendorApiUrl vendorApiUrl) : base(apiClientService)
        {
            _vendorApiUrl = vendorApiUrl;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<VendorViewModel>>(_vendorApiUrl.GetUrl(VendorEndpoint.GetAll));
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<VendorViewModel>());
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateVendorViewModel model)
        {
            try
            {
                var result = await _apiClientService.PostAsync<object>(_vendorApiUrl.GetUrl(VendorEndpoint.Create), model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            }
            return RedirectToAction("Index", "Vendor");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateVendorViewModel>(_vendorApiUrl.GetUrl(VendorEndpoint.GetById, id) );
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "Vendor");
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateVendorViewModel model)
        {
            try
            {
                var result = await _apiClientService.PutAsync<object>(_vendorApiUrl.GetUrl(VendorEndpoint.Update), model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "Vendor");
        }

        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_vendorApiUrl.GetUrl(VendorEndpoint.Delete, id) );
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "Vendor");
        }

        [HttpGet("Active/{id}")]
        public async Task<IActionResult> Active(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_vendorApiUrl.GetUrl(VendorEndpoint.Active, id) );
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "Vendor");
        }

        [HttpGet("DeActive/{id}")]
        public async Task<IActionResult> DeActive(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_vendorApiUrl.GetUrl(VendorEndpoint.DeActive,id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "Vendor");
        }
    }
}
