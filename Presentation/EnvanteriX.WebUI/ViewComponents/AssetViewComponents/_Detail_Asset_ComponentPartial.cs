using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.AssetViewComponents
{
    public class _Detail_Asset_ComponentPartial:ViewComponent
    {
        private readonly IApiClientService _apiClientService;
        private readonly AssetApiUrl _AssetApiUrl;
        public _Detail_Asset_ComponentPartial(IApiClientService apiClientService, AssetApiUrl assetApiUrl)
        {
            _apiClientService = apiClientService;
            _AssetApiUrl = assetApiUrl;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var values = await _apiClientService.GetAsync<AssetViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById,id));
                return View(values);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new AssetViewModel());
        }
    }
}
