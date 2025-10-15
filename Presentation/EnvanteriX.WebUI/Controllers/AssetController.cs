using EnvanteriX.WebUI.Enums;
using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using EnvanteriX.WebUI.ViewModels.AssetMovement;
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
        private readonly AssetMovementApiUrl _assetMovementApiUrl;
        private readonly AssetTypeApiUrl _assetTypeApiUrl;
        private readonly BrandApiUrl _brandApiUrl;
        private readonly ModelApiUrl _modelApiUrl;
        private readonly VendorApiUrl _vendorApiUrl;
        private readonly LocationApiUrl _locationApiUrl;
        private readonly UserApiUrl _userApiUrl;
        public AssetController(IApiClientService apiClientService, AssetApiUrl AssetApiUrl, AssetTypeApiUrl assetTypeApiUrl, BrandApiUrl brandApiUrl, ModelApiUrl modelApiUrl, VendorApiUrl vendorApiUrl, LocationApiUrl locationApiUrl, UserApiUrl userApiUrl, AssetMovementApiUrl assetMovementApiUrl) : base(apiClientService)
        {
            _AssetApiUrl = AssetApiUrl;
            _assetTypeApiUrl = assetTypeApiUrl;
            _brandApiUrl = brandApiUrl;
            _modelApiUrl = modelApiUrl;
            _vendorApiUrl = vendorApiUrl;
            _locationApiUrl = locationApiUrl;
            _userApiUrl = userApiUrl;
            _assetMovementApiUrl = assetMovementApiUrl;
        }
        private void Populate()
        {
            PopulateBrands();

            PopulateModels();

            PopulateAssetTypes();

            PopulateVendors();

            PopulateLocations();

            PopulateStatus();

            PopulateUsers();
        }
        private void PopulateBrands()
        {
            var Brands = _apiClientService.GetAsync<List<BrandViewModel>>(_brandApiUrl.GetUrl(BrandEndpoint.GetAllActive)).Result;
            ViewBag.Brands = new SelectList(Brands, "Id", "BrandName");
        }
        private void PopulateModels()
        {
            var Models = _apiClientService.GetAsync<List<ModelViewModel>>(_modelApiUrl.GetUrl(ModelEndpoint.GetAllActive)).Result;
            ViewBag.Models = new SelectList(Models, "Id", "ModelName");
        }
        private void PopulateAssetTypes()
        {
            var AssetTypes = _apiClientService.GetAsync<List<AssetTypeViewModel>>(_assetTypeApiUrl.GetUrl(AssetTypeEndpoint.GetAllActive)).Result;
            ViewBag.AssetTypes = new SelectList(AssetTypes, "Id", "TypeName");
        }
        private void PopulateVendors()
        {
            var Vendors = _apiClientService.GetAsync<List<VendorViewModel>>(_vendorApiUrl.GetUrl(VendorEndpoint.GetAllActive)).Result;
            ViewBag.Vendors = new SelectList(Vendors, "Id", "VendorName");
        }
        private void PopulateLocations()
        {
            var Locations = _apiClientService.GetAsync<List<LocationViewModel>>(_locationApiUrl.GetUrl(LocationEndpoint.GetAllActive)).Result;
            var locationList = Locations.Select(x => new
            {
                Id = x.Id,
                Name = $"{x.Building}"
            }).ToList();
            ViewBag.Locations = new SelectList(locationList, "Id", "Name");
        }
        private void PopulateStatus()
        {
            // Enum değerlerini ve Description'larını al
            ViewBag.Status = Enum.GetValues(typeof(StatusEnum))
                                 .Cast<StatusEnum>()
                                 .Select(s => new SelectListItem
                                 {
                                     Value = ((int)s).ToString(),
                                     Text = GetEnumDescription(s)
                                 }).ToList();
        }
        private void PopulateUsers()
        {
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
            var models = _apiClientService.GetAsync<List<ModelViewModel>>(_modelApiUrl.GetUrl(ModelEndpoint.GetAllActiveByBrandId, brandId)).Result;
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
            return View(new DetailAssetViewModel { Id = id });
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

                var result = await _apiClientService.PostAsync<CreateAssetResultViewModel>(
                    _AssetApiUrl.GetUrl(AssetEndpoint.Create), model);

                if (result != null)
                {

                    var movement = new CreateAssetMovementViewModel
                    {
                        AssetId = result.Id,
                        ToLocationId = model.LocationId,
                        ToUserId = model.AssignedUserId, // boşsa null olarak kalır
                        Note = "Yeni varlık eklendi."
                    };

                    await _apiClientService.PostAsync<object>(
                        _assetMovementApiUrl.GetUrl(AssetMovementEndpoint.Create), movement);
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                Populate();
                return RedirectAfterPost(true,model);
            }
            return RedirectAfterPost(false);
        }


        [HttpGet("Assign/{id}")]
        public async Task<IActionResult> Assign(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateAssetAssignViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById, id));
                if (value == null)
                {
                    throw new Exception("Varlık bulunamadı");
                }
                if (value.IsDeleted)
                {
                    throw new Exception("Varlık pasif olduğu için zimmetlemesi yapılamamaktadır.");
                }
                if (value.Status == StatusEnum.KullanimDisi || value.Status == StatusEnum.Tamirde)
                {
                    throw new Exception("Varlık durumu " + value.Status + " olduğu için zimmetlemesi yapılamamaktadır.");
                }
                PopulateUsers();
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
        }

        [HttpPost("Assign")]
        public async Task<IActionResult> Assign(UpdateAssetAssignViewModel model)
        {
            try
            {

                var current = await _apiClientService.GetAsync<UpdateAssetViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById, model.Id));
                if (current == null)
                {
                    throw new Exception("Varlık bulunamadı");
                }
                if (current.Status == StatusEnum.KullanimDisi || current.Status == StatusEnum.Tamirde)
                {
                    throw new Exception("Varlık durumu " + current.Status + " olduğu için zimmetlemesi yapılamamaktadır.");
                }
                current.AssignedUserId = model.AssignedUserId;
                if (current.AssignedUserId != null)
                {
                    current.Status = StatusEnum.Kullanimda; // Atama yapıldığında durumu "Kullanımda" olarak güncelle
                }
                else
                {
                    current.Status = StatusEnum.Stokta; // Atama yapıldığında durumu "Kullanımda" olarak güncelle
                }
                var result = await _apiClientService.PutAsync<object>(_AssetApiUrl.GetUrl(AssetEndpoint.Update), current);
                var movement = new CreateAssetMovementViewModel
                {
                    AssetId = model.Id,
                    FromLocationId = current.LocationId,
                    FromUserId = current.AssignedUserId,
                    ToLocationId = current.LocationId,
                    ToUserId = model.AssignedUserId, // boşsa null olarak kalır
                    Note = "Varlık Personele Atandı"
                };

                await _apiClientService.PostAsync<object>(
                    _assetMovementApiUrl.GetUrl(AssetMovementEndpoint.Create), movement);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                PopulateUsers();
                return RedirectAfterPost(true,model);
            }
            return RedirectAfterPost(false);
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
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateAssetViewModel model)
        {
            try
            {
                var current = await _apiClientService.GetAsync<UpdateAssetViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById, model.Id));
                model.Status = current.Status;
                var result = await _apiClientService.PutAsync<object>(_AssetApiUrl.GetUrl(AssetEndpoint.Update), model);
                var movement = new CreateAssetMovementViewModel
                {
                    AssetId = model.Id,
                    FromLocationId = current.LocationId,
                    FromUserId = current.AssignedUserId,
                    ToLocationId = model.LocationId,
                    ToUserId = model.AssignedUserId, // boşsa null olarak kalır
                    Note = "Varlık Düzenleme işlemi yapıldı."
                };

                await _apiClientService.PostAsync<object>(
                    _assetMovementApiUrl.GetUrl(AssetMovementEndpoint.Create), movement);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                Populate();
                return RedirectAfterPost(true,model);
            }
            return RedirectAfterPost(false);
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
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
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
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
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
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
        }
    }
}
