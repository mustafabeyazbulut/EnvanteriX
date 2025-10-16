using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers;

[Route("Home")]
public class HomeController : BaseController
{
    private readonly AssetApiUrl _assetApiUrl;
    public HomeController(IApiClientService apiClientService, AssetApiUrl assetApiUrl) : base(apiClientService)
    {
        _assetApiUrl = assetApiUrl;
    }

    [HttpGet("")]
    [HttpGet("Dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var value=await _apiClientService.GetAsync<AssetSummaryViewModel>(_assetApiUrl.GetUrl(AssetEndpoint.GetSummary));
        return View(value);
    }

    [HttpPost("ClearTempMessages")]
    public IActionResult ClearTempMessages()
    {
        TempData["SuccessMessage"] = null;
        TempData["ErrorMessage"] = null;
        return Ok();
    }


}
