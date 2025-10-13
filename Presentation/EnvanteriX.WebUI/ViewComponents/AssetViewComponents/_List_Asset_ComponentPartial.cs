using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.AssetViewComponents
{
    public class _List_Asset_ComponentPartial:ViewComponent
    {
        private readonly IApiClientService _apiClientService;
        private readonly AssetApiUrl _AssetApiUrl;

        public _List_Asset_ComponentPartial(IApiClientService apiClientService, AssetApiUrl assetApiUrl)
        {
            _apiClientService = apiClientService;
            _AssetApiUrl = assetApiUrl;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<AssetViewModel>>(_AssetApiUrl.GetUrl(AssetEndpoint.GetAll));
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<AssetViewModel>());
        }
    }
}
