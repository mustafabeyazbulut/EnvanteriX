using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("Role")]
    public class RoleController : BaseController
    {
        private readonly RoleApiUrl _roleApiUrl;

        public RoleController(IApiClientService apiClientService, RoleApiUrl roleApiUrl) : base(apiClientService)
        {
            _roleApiUrl = roleApiUrl;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<RoleViewModel>>(_roleApiUrl.GetUrl(RoleEndpoint.GetAll));
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<RoleViewModel>());
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateRoleViewModel model)
        {
            try
            {
                var result = await _apiClientService.PostAsync<object>(_roleApiUrl.GetUrl(RoleEndpoint.Create), model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true,model);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateRoleViewModel>(_roleApiUrl.GetUrl(RoleEndpoint.GetById, id));
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
        public async Task<IActionResult> Edit(UpdateRoleViewModel model)
        {
            try
            {
                var result = await _apiClientService.PutAsync<object>(_roleApiUrl.GetUrl(RoleEndpoint.Update), model);
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
                var result = await _apiClientService.DeleteAsync<object>(_roleApiUrl.GetUrl(RoleEndpoint.Delete, id));
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
                var result = await _apiClientService.DeleteAsync<object>(_roleApiUrl.GetUrl(RoleEndpoint.Active, id));
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
                var result = await _apiClientService.DeleteAsync<object>(_roleApiUrl.GetUrl(RoleEndpoint.DeActive, id));
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
