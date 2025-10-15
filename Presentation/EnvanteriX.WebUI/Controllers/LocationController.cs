using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Location;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("Location")]
    public class LocationController : BaseController
    {
        private readonly LocationApiUrl _LocationApiUrl;
        public LocationController(IApiClientService apiClientService, LocationApiUrl LocationApiUrl) : base(apiClientService)
        {
            _LocationApiUrl = LocationApiUrl;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<LocationViewModel>>(_LocationApiUrl.GetUrl(LocationEndpoint.GetAll));
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<LocationViewModel>());
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateLocationViewModel model)
        {
            try
            {
                var result = await _apiClientService.PostAsync<object>(_LocationApiUrl.GetUrl(LocationEndpoint.Create), model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true, model);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateLocationViewModel>(_LocationApiUrl.GetUrl(LocationEndpoint.GetById, id));
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateLocationViewModel model)
        {
            try
            {
                var result = await _apiClientService.PutAsync<object>(_LocationApiUrl.GetUrl(LocationEndpoint.Update), model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true, model);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_LocationApiUrl.GetUrl(LocationEndpoint.Delete, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("Active/{id}")]
        public async Task<IActionResult> Active(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_LocationApiUrl.GetUrl(LocationEndpoint.Active, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("DeActive/{id}")]
        public async Task<IActionResult> DeActive(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_LocationApiUrl.GetUrl(LocationEndpoint.DeActive, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
        }
    }
}
