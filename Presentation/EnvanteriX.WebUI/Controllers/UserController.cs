using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Home;
using EnvanteriX.WebUI.ViewModels.Role;
using EnvanteriX.WebUI.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("User")]
    [Authorize(Roles = "admin")]
    public class UserController : BaseController
    {
        private readonly UserApiUrl _UserApiUrl;
        private readonly RoleApiUrl _roleApiUrl;
        private readonly AuthApiUrl _authApiUrl;
        public UserController(IApiClientService apiClientService, ILogger<BaseController> logger, UserApiUrl userApiUrl, RoleApiUrl roleApiUrl, AuthApiUrl authApiUrl) : base(apiClientService, logger)
        {
            _UserApiUrl = userApiUrl;
            _roleApiUrl = roleApiUrl;
            _authApiUrl = authApiUrl;
        }

        private void PopulateRoles()
        {
            var roles = _apiClientService.GetAsync<List<RoleViewModel>>(_roleApiUrl.GetUrl(RoleEndpoint.GetAllActive)).Result;
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
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
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                PopulateRoles();
                return RedirectAfterPost(true,User);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("Profile/{id}")]
        public async Task<IActionResult> Profile(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateUserViewModel>(_UserApiUrl.GetUrl(UserEndpoint.GetById, id));

                return View(new ProfileViewModel
                {
                    email = value.Email,
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
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
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
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
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                PopulateRoles();
                return RedirectAfterPost(true,User);
            }
            return RedirectAfterPost(false);
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
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
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
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
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
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<ChangePasswordViewModel>(_UserApiUrl.GetUrl(UserEndpoint.GetById, id));
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                await _apiClientService.PutAsync<object>(_UserApiUrl.GetUrl(UserEndpoint.ChangePassword), model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true, User);
            }
            return RedirectAfterPost(false);
        }
    }
}
