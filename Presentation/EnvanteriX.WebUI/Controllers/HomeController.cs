using EnvanteriX.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers;

[Route("Home")]
public class HomeController : BaseController
{
    public HomeController(IApiClientService apiClientService) : base(apiClientService)
    {
    }

    [HttpGet("")]
    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        return View();
    }

}
