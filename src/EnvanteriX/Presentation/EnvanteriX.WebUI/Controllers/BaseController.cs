using EnvanteriX.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IApiClientService _apiClientService;

        public BaseController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }
    }
}
