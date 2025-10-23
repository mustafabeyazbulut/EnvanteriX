using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.UserViewComponents
{
    public class _GetByEmail_User_ComponentPartial:ViewComponent
    {
        private readonly IApiClientService _apiClientService;
        private readonly UserApiUrl _userApiUrl;
        public _GetByEmail_User_ComponentPartial(IApiClientService apiClientService, UserApiUrl userApiUrl)
        {
            _apiClientService = apiClientService;
            _userApiUrl = userApiUrl;
        }
        public async Task<IViewComponentResult> InvokeAsync(string email)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UserViewModel>(_userApiUrl.GetUrl(UserEndpoint.GetByEmail) + email);
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new UserViewModel());
        }
    }
}
