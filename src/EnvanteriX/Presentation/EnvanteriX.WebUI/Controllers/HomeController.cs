using EnvanteriX.WebUI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers;

[Route("Home")]
public class HomeController : BaseController
{

    public HomeController(IApiClientService apiClientService) : base(apiClientService)
    {
    }

    [Authorize(Roles = "user")]
    [HttpGet("")]
    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        return View();
    }

}
