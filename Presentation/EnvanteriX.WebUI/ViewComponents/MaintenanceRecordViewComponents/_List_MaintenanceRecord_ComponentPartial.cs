using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.MaintenanceRecord;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.MaintenanceRecordViewComponents
{
    /// <summary>
    /// Sayfalama ve filtreleme destekli bakım kayıtları listesi view component
    /// </summary>
    public class _List_MaintenanceRecord_ComponentPartial : ViewComponent
    {
        private readonly IApiClientService _apiClientService;
        private readonly VendorApiUrl _vendorApiUrl;
        private readonly MaintenanceRecordApiUrl _maintenanceRecordApiUrl;

        public _List_MaintenanceRecord_ComponentPartial(
            IApiClientService apiClientService,
            MaintenanceRecordApiUrl maintenanceRecordApiUrl,
            VendorApiUrl vendorApiUrl)
        {
            _apiClientService = apiClientService;
            _maintenanceRecordApiUrl = maintenanceRecordApiUrl;
            _vendorApiUrl = vendorApiUrl;
        }

        public async Task<IViewComponentResult> InvokeAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            int? assetId = null,
            int? vendorId = null,
            DateTime? startDateFrom = null,
            DateTime? startDateTo = null,
            bool? isCompleted = null,
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

                if (assetId.HasValue)
                    queryParams.Add($"assetId={assetId.Value}");

                if (vendorId.HasValue)
                    queryParams.Add($"vendorId={vendorId.Value}");

                if (startDateFrom.HasValue)
                    queryParams.Add($"startDateFrom={startDateFrom.Value:yyyy-MM-dd}");

                if (startDateTo.HasValue)
                    queryParams.Add($"startDateTo={startDateTo.Value:yyyy-MM-dd}");

                if (isCompleted.HasValue)
                    queryParams.Add($"isCompleted={isCompleted.Value}");

                if (isDeleted.HasValue)
                    queryParams.Add($"isDeleted={isDeleted.Value}");

                var queryString = string.Join("&", queryParams);
                var url = $"{_maintenanceRecordApiUrl.GetUrl(MaintenanceRecordEndpoint.GetAllPaginated)}?{queryString}";

                var paginatedResult = await _apiClientService.GetAsync<PaginatedMaintenanceRecordViewModel>(url);
                var viewModel = paginatedResult ?? new PaginatedMaintenanceRecordViewModel();

                // Vendor Dropdown Doldurma
                try
                {
                    var vendors = await _apiClientService.GetAsync<List<EnvanteriX.WebUI.ViewModels.Vendor.VendorViewModel>>(_vendorApiUrl.GetUrl(VendorEndpoint.GetAllActive));
                    if (vendors != null)
                    {
                         viewModel.Vendors = vendors.Select(v => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                         {
                             Text = v.VendorName,
                             Value = v.Id.ToString(),
                             Selected = vendorId.HasValue && vendorId.Value == v.Id
                         }).ToList();
                    }
                }
                catch (Exception ex)
                {
                   // Log
                }
                
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Bakım kayıtları yüklenirken hata oluştu: {ex.Message}";
                return View(new PaginatedMaintenanceRecordViewModel());
            }
        }
    }
}
