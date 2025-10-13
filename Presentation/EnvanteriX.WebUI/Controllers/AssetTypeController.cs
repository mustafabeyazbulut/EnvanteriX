using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.AssetType;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("AssetType")]
    public class AssetTypeController : BaseController
    {
        private readonly AssetTypeApiUrl _AssetTypeApiUrl;
        public AssetTypeController(IApiClientService apiClientService, AssetTypeApiUrl AssetTypeApiUrl) : base(apiClientService)
        {
            _AssetTypeApiUrl = AssetTypeApiUrl;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<AssetTypeViewModel>>(_AssetTypeApiUrl.GetUrl(AssetTypeEndpoint.GetAll));
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<AssetTypeViewModel>());
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateAssetTypeViewModel model)
        {
            try
            {
                var result = await _apiClientService.PostAsync<object>(_AssetTypeApiUrl.GetUrl(AssetTypeEndpoint.Create), model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            }
            return RedirectToAction("Index", "AssetType");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateAssetTypeViewModel>(_AssetTypeApiUrl.GetUrl(AssetTypeEndpoint.GetById, id));
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "AssetType");
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateAssetTypeViewModel model)
        {
            try
            {
                var result = await _apiClientService.PutAsync<object>(_AssetTypeApiUrl.GetUrl(AssetTypeEndpoint.Update), model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "AssetType");
        }

        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_AssetTypeApiUrl.GetUrl(AssetTypeEndpoint.Delete, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "AssetType");
        }

        [HttpGet("Active/{id}")]
        public async Task<IActionResult> Active(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_AssetTypeApiUrl.GetUrl(AssetTypeEndpoint.Active, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "AssetType");
        }

        [HttpGet("DeActive/{id}")]
        public async Task<IActionResult> DeActive(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_AssetTypeApiUrl.GetUrl(AssetTypeEndpoint.DeActive, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "AssetType");
        }
    }
}
