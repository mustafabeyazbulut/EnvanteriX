using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using EnvanteriX.WebUI.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers;

[Authorize(Roles = "user,admin")]
[Route("Home")]
public class HomeController : BaseController
{
    private readonly AssetApiUrl _assetApiUrl;
    public HomeController(IApiClientService apiClientService, ILogger<BaseController> logger, AssetApiUrl assetApiUrl)
        : base(apiClientService, logger)
    {
        _assetApiUrl = assetApiUrl;
    }

    // Giriþ noktasý: role göre yönlendir
    [HttpGet("")]
    [HttpGet("Index")]
    [Authorize(Roles = "user,admin")]
    public IActionResult Index()
    {
        if (User.IsInRole("admin"))
            return RedirectToAction("Dashboard");
        else
            return RedirectToAction("Profile");
    }

    [HttpGet("Dashboard")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Dashboard()
    {
        var value = await _apiClientService.GetAsync<AssetSummaryViewModel>(_assetApiUrl.GetUrl(AssetEndpoint.GetSummary));
        return View(value);
    }

    [HttpGet("Profile")]
    [Authorize(Roles = "user,admin")]
    public async Task<IActionResult> Profile()
    {
        var email = User.Identity?.Name;
        return View(new ProfileViewModel
        {
            email= email
        });
    }

    [HttpPost("ClearTempMessages")]
    public IActionResult ClearTempMessages()
    {
        TempData["SuccessMessage"] = null;
        TempData["ErrorMessage"] = null;

        var previousUrl = TempData["PreviousUrl"]?.ToString() ?? "/";
        TempData.Keep("PreviousUrl");

        return Json(new { previousUrl });
    }
}
