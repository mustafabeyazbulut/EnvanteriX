using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.AssetViewComponents
{
    public class _User_Asset_ComponentPartial: ViewComponent
    {
        private readonly IApiClientService _apiClientService;
        private readonly AssetApiUrl _assetApiUrl;
        public _User_Asset_ComponentPartial(IApiClientService apiClientService, AssetApiUrl assetApiUrl)
        {
            _apiClientService = apiClientService;
            _assetApiUrl = assetApiUrl;
        }
        public async Task<IViewComponentResult> InvokeAsync(string userEmail)
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<UserAssetViewModel>>(_assetApiUrl.GetUrl(AssetEndpoint.GetAllActiveByEmail)+userEmail);
                return View(values);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<UserAssetViewModel>());
        }
    }
}
