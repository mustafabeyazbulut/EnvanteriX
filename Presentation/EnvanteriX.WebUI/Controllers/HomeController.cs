using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers;

[Route("Home")]
public class HomeController : BaseController
{
    private readonly AssetApiUrl _assetApiUrl;

    public HomeController(IApiClientService apiClientService, ILogger<BaseController> logger, AssetApiUrl assetApiUrl) : base(apiClientService, logger)
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

        // PreviousUrl'i geri döndür
        var previousUrl = TempData["PreviousUrl"]?.ToString() ?? "/";
        TempData.Keep("PreviousUrl"); // Bir sonraki istek için koru

        return Json(new { previousUrl });
    }


}
