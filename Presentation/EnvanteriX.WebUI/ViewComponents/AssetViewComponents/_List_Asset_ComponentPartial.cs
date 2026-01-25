using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.AssetViewComponents
{
    /// <summary>
    /// Sayfalama ve filtreleme destekli varlık listesi view component
    /// </summary>
    public class _List_Asset_ComponentPartial : ViewComponent
    {
        private readonly IApiClientService _apiClientService;
        private readonly AssetApiUrl _assetApiUrl;
        private readonly AssetTypeApiUrl _assetTypeApiUrl;
        private readonly BrandApiUrl _brandApiUrl;
        private readonly ModelApiUrl _modelApiUrl;
        private readonly LocationApiUrl _locationApiUrl;
        private readonly VendorApiUrl _vendorApiUrl;
        private readonly DepartmentApiUrl _departmentApiUrl;
        private readonly UserApiUrl _userApiUrl; // Varsayılan

        public _List_Asset_ComponentPartial(
            IApiClientService apiClientService, 
            AssetApiUrl assetApiUrl,
            AssetTypeApiUrl assetTypeApiUrl,
            BrandApiUrl brandApiUrl,
            ModelApiUrl modelApiUrl,
            LocationApiUrl locationApiUrl,
            VendorApiUrl vendorApiUrl,
            DepartmentApiUrl departmentApiUrl,
            UserApiUrl userApiUrl)
        {
            _apiClientService = apiClientService;
            _assetApiUrl = assetApiUrl;
            _assetTypeApiUrl = assetTypeApiUrl;
            _brandApiUrl = brandApiUrl;
            _modelApiUrl = modelApiUrl;
            _locationApiUrl = locationApiUrl;
            _vendorApiUrl = vendorApiUrl;
            _departmentApiUrl = departmentApiUrl;
            _userApiUrl = userApiUrl;
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
                
                var viewModel = paginatedResult ?? new PaginatedAssetViewModel();

                // Dropdownları Doldur
                try
                {
                    // Asset Types
                    var assetTypes = await _apiClientService.GetAsync<List<EnvanteriX.WebUI.ViewModels.AssetType.AssetTypeViewModel>>(_assetTypeApiUrl.GetUrl(AssetTypeEndpoint.GetAllActive));
                    if(assetTypes != null)
                        viewModel.AssetTypes = assetTypes.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.TypeName, Value = x.Id.ToString(), Selected = assetTypeId == x.Id }).ToList();

                    // Brands
                    var brands = await _apiClientService.GetAsync<List<EnvanteriX.WebUI.ViewModels.Brand.BrandViewModel>>(_brandApiUrl.GetUrl(BrandEndpoint.GetAllActive));
                    if(brands != null)
                        viewModel.Brands = brands.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.BrandName, Value = x.Id.ToString(), Selected = brandId == x.Id }).ToList();

                    // Models
                    // Modeller normalde marka seçilince gelir ama filtrelemede tüm modeller mi gelsin? 
                    // İsterseniz tüm modelleri çekelim.
                    var models = await _apiClientService.GetAsync<List<EnvanteriX.WebUI.ViewModels.Model.ModelViewModel>>(_modelApiUrl.GetUrl(ModelEndpoint.GetAllActive));
                    if(models != null)
                        viewModel.Models = models.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.ModelName, Value = x.Id.ToString(), Selected = modelId == x.Id }).ToList();
                        
                    // Locations
                    var locations = await _apiClientService.GetAsync<List<EnvanteriX.WebUI.ViewModels.Location.LocationViewModel>>(_locationApiUrl.GetUrl(LocationEndpoint.GetAllActive));
                    if(locations != null)
                        viewModel.Locations = locations.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.Building, Value = x.Id.ToString(), Selected = locationId == x.Id }).ToList();

                    // Vendors
                    var vendors = await _apiClientService.GetAsync<List<EnvanteriX.WebUI.ViewModels.Vendor.VendorViewModel>>(_vendorApiUrl.GetUrl(VendorEndpoint.GetAllActive));
                     if(vendors != null)
                        viewModel.Vendors = vendors.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.VendorName, Value = x.Id.ToString(), Selected = vendorId == x.Id }).ToList();

                    // Departments
                    var departments = await _apiClientService.GetAsync<List<EnvanteriX.WebUI.ViewModels.Department.DepartmentViewModel>>(_departmentApiUrl.GetUrl(DepartmentEndpoint.GetAllActive));
                     if(departments != null)
                        viewModel.Departments = departments.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = assignedDepartmentId == x.Id }).ToList();

                    // Users
                     var users = await _apiClientService.GetAsync<List<EnvanteriX.WebUI.ViewModels.User.UserViewModel>>(_userApiUrl.GetUrl(UserEndpoint.GetAllActive));
                     if(users != null)
                        viewModel.Users = users.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.FullName, Value = x.Id.ToString(), Selected = assignedUserId == x.Id }).ToList();
                }
                catch
                {
                    // Dropdown yükleme hatası listeyi engellememeli
                }
                
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Varlıklar yüklenirken hata oluştu: {ex.Message}";
                return View(new PaginatedAssetViewModel());
            }
        }
    }
}
