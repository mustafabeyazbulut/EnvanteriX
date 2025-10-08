using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Role;
using EnvanteriX.WebUI.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("User")]
    public class UserController : BaseController
    {
        private readonly UserApiUrl _UserApiUrl;
        private readonly RoleApiUrl _roleApiUrl;
        private readonly AuthApiUrl _authApiUrl;
        public UserController(IApiClientService apiClientService, UserApiUrl UserApiUrl, RoleApiUrl roleApiUrl, AuthApiUrl authApiUrl) : base(apiClientService)
        {
            _UserApiUrl = UserApiUrl;
            _roleApiUrl = roleApiUrl;
            _authApiUrl = authApiUrl;
        }
        private void PopulateRoles()
        {
            var roles = _apiClientService.GetAsync<List<RoleViewModel>>(_roleApiUrl.GetUrl(RoleEndpoint.GetAllActive)).Result;
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<UserViewModel>>(_UserApiUrl.GetUrl(UserEndpoint.GetAll));
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
            PopulateRoles();
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateUserViewModel User)
        {
            try
            {
                var role = await _apiClientService.GetAsync<RoleViewModel>(_roleApiUrl.GetUrl(RoleEndpoint.GetById, User.RoleId));
                User.Role = role.Name;
                var result = await _apiClientService.PostAsync<object>(_authApiUrl.GetUrl(AuthEndpoint.Register), User);
                PopulateRoles();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateUserViewModel>(_UserApiUrl.GetUrl(UserEndpoint.GetById, id));
                PopulateRoles();
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateUserViewModel User)
        {
            try
            {
                await _apiClientService.PutAsync<object>(_UserApiUrl.GetUrl(UserEndpoint.Update), User);
                var role =await _apiClientService.GetAsync<RoleViewModel>(_roleApiUrl.GetUrl(RoleEndpoint.GetById, User.RoleId));
                await _apiClientService.PostAsync<object>(_UserApiUrl.GetUrl(UserEndpoint.AddRole), new UpdateUserRoleViewModel
                {
                    RoleName = role.Name,
                    UserId = User.Id
                });
                PopulateRoles();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_UserApiUrl.GetUrl(UserEndpoint.Delete, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet("Active/{id}")]
        public async Task<IActionResult> Active(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_UserApiUrl.GetUrl(UserEndpoint.Active, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet("DeActive/{id}")]
        public async Task<IActionResult> DeActive(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_UserApiUrl.GetUrl(UserEndpoint.DeActive, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "User");
        }
    }
}
