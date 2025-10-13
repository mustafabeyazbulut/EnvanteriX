using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.AssetMovement;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.AssetMovementViewComponents
{
    public class _GetAllByAssetId_AssetMovement_ComponentPartial:ViewComponent
    {
        private readonly IApiClientService _apiClientService;
        private readonly AssetMovementApiUrl _assetMovementApiUrl;

        public _GetAllByAssetId_AssetMovement_ComponentPartial(AssetMovementApiUrl assetMovementApiUrl, IApiClientService apiClientService)
        {
            _assetMovementApiUrl = assetMovementApiUrl;
            _apiClientService = apiClientService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<AssetMovementViewModel>>(_assetMovementApiUrl.GetUrl(AssetMovementEndpoint.GetAllByAssetId,id));
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<AssetMovementViewModel>());
        }
    }
}
