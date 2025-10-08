using EnvanteriX.WebUI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class BaseController : Controller
    {
        protected readonly IApiClientService _apiClientService;

        public BaseController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }
    }
}
