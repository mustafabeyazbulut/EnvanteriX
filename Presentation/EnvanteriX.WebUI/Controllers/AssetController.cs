using EnvanteriX.WebUI.Enums;
using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using EnvanteriX.WebUI.ViewModels.AssetType;
using EnvanteriX.WebUI.ViewModels.Brand;
using EnvanteriX.WebUI.ViewModels.Location;
using EnvanteriX.WebUI.ViewModels.Model;
using EnvanteriX.WebUI.ViewModels.User;
using EnvanteriX.WebUI.ViewModels.Vendor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("Asset")]
    public class AssetController : BaseController
    {
        private readonly AssetApiUrl _AssetApiUrl;
        private readonly AssetTypeApiUrl _assetTypeApiUrl;
        private readonly BrandApiUrl _brandApiUrl;
        private readonly ModelApiUrl _modelApiUrl;
        private readonly VendorApiUrl _vendorApiUrl;
        private readonly LocationApiUrl _locationApiUrl;
        private readonly UserApiUrl _userApiUrl;
        public AssetController(IApiClientService apiClientService, AssetApiUrl AssetApiUrl, AssetTypeApiUrl assetTypeApiUrl, BrandApiUrl brandApiUrl, ModelApiUrl modelApiUrl, VendorApiUrl vendorApiUrl, LocationApiUrl locationApiUrl, UserApiUrl userApiUrl) : base(apiClientService)
        {
            _AssetApiUrl = AssetApiUrl;
            _assetTypeApiUrl = assetTypeApiUrl;
            _brandApiUrl = brandApiUrl;
            _modelApiUrl = modelApiUrl;
            _vendorApiUrl = vendorApiUrl;
            _locationApiUrl = locationApiUrl;
            _userApiUrl = userApiUrl;
        }
        private void Populate()
        {
            var Brands = _apiClientService.GetAsync<List<BrandViewModel>>(_brandApiUrl.GetUrl(BrandEndpoint.GetAllActive)).Result;
            ViewBag.Brands = new SelectList(Brands, "Id", "BrandName");

            var Models = _apiClientService.GetAsync<List<ModelViewModel>>(_modelApiUrl.GetUrl(ModelEndpoint.GetAllActive)).Result;
            ViewBag.Models = new SelectList(Models, "Id", "ModelName");

            var AssetTypes = _apiClientService.GetAsync<List<AssetTypeViewModel>>(_assetTypeApiUrl.GetUrl(AssetTypeEndpoint.GetAllActive)).Result;
            ViewBag.AssetTypes = new SelectList(AssetTypes, "Id", "TypeName");

            var Vendors = _apiClientService.GetAsync<List<VendorViewModel>>(_vendorApiUrl.GetUrl(VendorEndpoint.GetAllActive)).Result;
            ViewBag.Vendors = new SelectList(Vendors, "Id", "VendorName");

            var Locations = _apiClientService.GetAsync<List<LocationViewModel>>(_locationApiUrl.GetUrl(LocationEndpoint.GetAllActive)).Result;
            var locationList = Locations.Select(x => new
            {
                Id = x.Id,
                Name = $"{x.Building} - {x.Floor} - {x.Room}"
            }).ToList();
            ViewBag.Locations = new SelectList(locationList, "Id", "Name");

            // Enum değerlerini ve Description'larını al
            ViewBag.Status = Enum.GetValues(typeof(StatusEnum))
                                 .Cast<StatusEnum>()
                                 .Select(s => new SelectListItem
                                 {
                                     Value = ((int)s).ToString(),
                                     Text = GetEnumDescription(s)
                                 }).ToList();

            var Users = _apiClientService.GetAsync<List<UserViewModel>>(_userApiUrl.GetUrl(UserEndpoint.GetAllActive)).Result;
            ViewBag.Users = new SelectList(Users, "Id", "FullName");
        }
        private string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        [HttpGet("GetModelsByBrand")]
        public JsonResult GetModelsByBrand(int brandId)
        {
            var models = _apiClientService.GetAsync<List<ModelViewModel>>(_modelApiUrl.GetUrl(ModelEndpoint.GetAllActiveByBrandId,brandId)).Result;
            ViewBag.Models = new SelectList(models, "Id", "ModelName");

            return Json(models);
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            return View(new DetailAssetViewModel { Id=id});
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            Populate();
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateAssetViewModel model)
        {
            try
            {
                var result = await _apiClientService.PostAsync<object>(_AssetApiUrl.GetUrl(AssetEndpoint.Create), model);
                Populate();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            }
            return RedirectToAction("Index", "Asset");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateAssetViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById, id));
                Populate();
                var models = _apiClientService.GetAsync<List<ModelViewModel>>(_modelApiUrl.GetUrl(ModelEndpoint.GetAllActiveByBrandId, value.BrandId)).Result;
                ViewBag.Models = new SelectList(models, "Id", "ModelName");
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "Asset");
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateAssetViewModel model)
        {
            try
            {
                var result = await _apiClientService.PutAsync<object>(_AssetApiUrl.GetUrl(AssetEndpoint.Update), model);
                Populate();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "Asset");
        }

        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_AssetApiUrl.GetUrl(AssetEndpoint.Delete, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "Asset");
        }

        [HttpGet("Active/{id}")]
        public async Task<IActionResult> Active(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_AssetApiUrl.GetUrl(AssetEndpoint.Active, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "Asset");
        }

        [HttpGet("DeActive/{id}")]
        public async Task<IActionResult> DeActive(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_AssetApiUrl.GetUrl(AssetEndpoint.DeActive, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("Index", "Asset");
        }
    }
}
