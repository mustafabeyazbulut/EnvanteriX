using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.AssetViewComponents
{
    /// <summary>
    /// Sayfalama ve filtreleme destekli varlık listesi view component
    /// </summary>
    public class _List_Asset_Paginated_ComponentPartial : ViewComponent
    {
        private readonly IApiClientService _apiClientService;
        private readonly AssetApiUrl _assetApiUrl;

        public _List_Asset_Paginated_ComponentPartial(
            IApiClientService apiClientService, 
            AssetApiUrl assetApiUrl)
        {
            _apiClientService = apiClientService;
            _assetApiUrl = assetApiUrl;
        }

        public async Task<IViewComponentResult> InvokeAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? assetTag = null,
            string? serialNumber = null,
            int? assetTypeId = null,
            int? brandId = null,
            int? modelId = null,
            int? locationId = null,
            int? vendorId = null,
            int? assignedUserId = null,
            int? assignedDepartmentId = null,
            string? status = null,
            bool? isRented = null,
            bool? isDeleted = null)
        {
            try
            {
                // Query string oluştur
                var queryParams = new List<string>
                {
                    $"pageNumber={pageNumber}",
                    $"pageSize={pageSize}"
                };

                if (!string.IsNullOrWhiteSpace(searchTerm))
                    queryParams.Add($"searchTerm={Uri.EscapeDataString(searchTerm)}");
                
                if (!string.IsNullOrWhiteSpace(assetTag))
                    queryParams.Add($"assetTag={Uri.EscapeDataString(assetTag)}");
                
                if (!string.IsNullOrWhiteSpace(serialNumber))
                    queryParams.Add($"serialNumber={Uri.EscapeDataString(serialNumber)}");
                
                if (assetTypeId.HasValue)
                    queryParams.Add($"assetTypeId={assetTypeId.Value}");
                
                if (brandId.HasValue)
                    queryParams.Add($"brandId={brandId.Value}");
                
                if (modelId.HasValue)
                    queryParams.Add($"modelId={modelId.Value}");
                
                if (locationId.HasValue)
                    queryParams.Add($"locationId={locationId.Value}");
                
                if (vendorId.HasValue)
                    queryParams.Add($"vendorId={vendorId.Value}");
                
                if (assignedUserId.HasValue)
                    queryParams.Add($"assignedUserId={assignedUserId.Value}");
                
                if (assignedDepartmentId.HasValue)
                    queryParams.Add($"assignedDepartmentId={assignedDepartmentId.Value}");
                
                if (!string.IsNullOrWhiteSpace(status))
                    queryParams.Add($"status={Uri.EscapeDataString(status)}");
                
                if (isRented.HasValue)
                    queryParams.Add($"isRented={isRented.Value}");
                
                if (isDeleted.HasValue)
                    queryParams.Add($"isDeleted={isDeleted.Value}");

                var queryString = string.Join("&", queryParams);
                var url = $"{_assetApiUrl.GetUrl(AssetEndpoint.GetAllPaginated)}?{queryString}";

                var paginatedResult = await _apiClientService.GetAsync<PaginatedAssetViewModel>(url);
                
                return View(paginatedResult ?? new PaginatedAssetViewModel());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Varlıklar yüklenirken hata oluştu: {ex.Message}";
                return View(new PaginatedAssetViewModel());
            }
        }
    }
}
