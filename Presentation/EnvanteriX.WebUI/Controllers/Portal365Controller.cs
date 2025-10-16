using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Portal365;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("Portal365")]
    public class Portal365Controller : BaseController
    {
        private readonly Portal365ApiUrl _Portal365ApiUrl;
        public Portal365Controller(IApiClientService apiClientService, Portal365ApiUrl Portal365ApiUrl) : base(apiClientService)
        {
            _Portal365ApiUrl = Portal365ApiUrl;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<Portal365ViewModel>>(_Portal365ApiUrl.GetUrl(Portal365Endpoint.GetAll));
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<Portal365ViewModel>());
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreatePortal365ViewModel model)
        {
            try
            {
                var result = await _apiClientService.PostAsync<object>(_Portal365ApiUrl.GetUrl(Portal365Endpoint.Create), model);
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
                var value = await _apiClientService.GetAsync<UpdatePortal365ViewModel>(_Portal365ApiUrl.GetUrl(Portal365Endpoint.GetById, id));
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdatePortal365ViewModel model)
        {
            try
            {
                var result = await _apiClientService.PutAsync<object>(_Portal365ApiUrl.GetUrl(Portal365Endpoint.Update), model);
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
                var result = await _apiClientService.DeleteAsync<object>(_Portal365ApiUrl.GetUrl(Portal365Endpoint.Delete, id));
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
                var result = await _apiClientService.DeleteAsync<object>(_Portal365ApiUrl.GetUrl(Portal365Endpoint.Active, id));
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
                var result = await _apiClientService.DeleteAsync<object>(_Portal365ApiUrl.GetUrl(Portal365Endpoint.DeActive, id));
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
