using EnvanteriX.WebUI.Attributes;
using EnvanteriX.WebUI.Enums;
using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Asset;
using EnvanteriX.WebUI.ViewModels.AssetMovement;
using EnvanteriX.WebUI.ViewModels.AssetType;
using EnvanteriX.WebUI.ViewModels.Brand;
using EnvanteriX.WebUI.ViewModels.Department;
using EnvanteriX.WebUI.ViewModels.Location;
using EnvanteriX.WebUI.ViewModels.Model;
using EnvanteriX.WebUI.ViewModels.User;
using EnvanteriX.WebUI.ViewModels.Vendor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("Asset")]
    [Authorize(Roles = "admin")]

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
        private readonly DepartmentApiUrl _departmentApiUrl;

        public AssetController(IApiClientService apiClientService, ILogger<BaseController> logger, AssetApiUrl assetApiUrl, AssetMovementApiUrl assetMovementApiUrl, AssetTypeApiUrl assetTypeApiUrl, BrandApiUrl brandApiUrl, ModelApiUrl modelApiUrl, VendorApiUrl vendorApiUrl, LocationApiUrl locationApiUrl, UserApiUrl userApiUrl, DepartmentApiUrl departmentApiUrl) : base(apiClientService, logger)
        {
            _AssetApiUrl = assetApiUrl;
            _assetMovementApiUrl = assetMovementApiUrl;
            _assetTypeApiUrl = assetTypeApiUrl;
            _brandApiUrl = brandApiUrl;
            _modelApiUrl = modelApiUrl;
            _vendorApiUrl = vendorApiUrl;
            _locationApiUrl = locationApiUrl;
            _userApiUrl = userApiUrl;
            _departmentApiUrl = departmentApiUrl;
        }

        private async Task PopulateAsync()
        {
            await PopulateBrandsAsync();
            await PopulateModelsAsync();
            await PopulateAssetTypesAsync();
            await PopulateVendorsAsync();
            await PopulateLocationsAsync();
            await PopulateUsersAsync();
            await PopulateDepartmentsAsync();
            PopulateStatus();
        }
        private async Task PopulateBrandsAsync()
        {
            var Brands = await _apiClientService.GetAsync<List<BrandViewModel>>(_brandApiUrl.GetUrl(BrandEndpoint.GetAllActive));
            ViewBag.Brands = new SelectList(Brands, "Id", "BrandName");
        }
        private async Task PopulateModelsAsync()
        {
            var Models = await _apiClientService.GetAsync<List<ModelViewModel>>(_modelApiUrl.GetUrl(ModelEndpoint.GetAllActive));
            ViewBag.Models = new SelectList(Models, "Id", "ModelName");
        }
        private async Task PopulateAssetTypesAsync()
        {
            var AssetTypes = await _apiClientService.GetAsync<List<AssetTypeViewModel>>(_assetTypeApiUrl.GetUrl(AssetTypeEndpoint.GetAllActive));
            ViewBag.AssetTypes = new SelectList(AssetTypes, "Id", "TypeName");
        }
        private async Task PopulateVendorsAsync()
        {
            var Vendors = await _apiClientService.GetAsync<List<VendorViewModel>>(_vendorApiUrl.GetUrl(VendorEndpoint.GetAllActive));
            ViewBag.Vendors = new SelectList(Vendors, "Id", "VendorName");
        }
        private async Task PopulateLocationsAsync()
        {
            var Locations = await _apiClientService.GetAsync<List<LocationViewModel>>(_locationApiUrl.GetUrl(LocationEndpoint.GetAllActive));
            var locationList = Locations.Select(x => new
            {
                Id = x.Id,
                Name = $"{x.Building}"
            }).ToList();
            ViewBag.Locations = new SelectList(locationList, "Id", "Name");
        }
        private void PopulateStatus()
        {
            ViewBag.Status = Enum.GetValues(typeof(StatusEnum))
                                 .Cast<StatusEnum>()
                                 .Where(s => s != StatusEnum.None)
                                 .Select(s => new SelectListItem
                                 {
                                     Value = ((int)s).ToString(),
                                     Text = GetEnumDescription(s)
                                 }).ToList();
        }
        private async Task PopulateUsersAsync()
        {
            var Users = await _apiClientService.GetAsync<List<UserViewModel>>(_userApiUrl.GetUrl(UserEndpoint.GetAllActive));
            ViewBag.Users = new SelectList(Users, "Id", "FullName");
        }
        private async Task PopulateDepartmentsAsync()
        {
            var Departments = await _apiClientService.GetAsync<List<DepartmentViewModel>>(_departmentApiUrl.GetUrl(DepartmentEndpoint.GetAllActive));
            ViewBag.Departments = new SelectList(Departments, "Id", "Name");
        }

        private string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        [HttpGet("GetModelsByBrand")]
        [SkipBaseActionFilter]
        public async Task<JsonResult> GetModelsByBrand(int brandId)
        {
            var models = await _apiClientService.GetAsync<List<ModelViewModel>>(_modelApiUrl.GetUrl(ModelEndpoint.GetAllActiveByBrandId, brandId));
            ViewBag.Models = new SelectList(models, "Id", "ModelName");

            return Json(models);
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(IndexPaginated));
        }

        /// <summary>
        /// Sayfalama ve filtreleme destekli varlık listesi sayfası
        /// </summary>
        [HttpGet("Paginated")]
        public async Task<IActionResult> IndexPaginated()
        {
            // Dropdown'lar için verileri hazırla
            await PopulateAsync();
            return View();
        }

        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            return View(new DetailAssetViewModel { Id = id });
        }

        [HttpGet("Add")]
        public async Task<IActionResult> Add()
        {
            await PopulateAsync();
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
                        ToDepartmentId = model.AssignedDepartmentId,
                        Note = "Yeni varlık eklendi."
                    };

                    await _apiClientService.PostAsync<object>(
                        _assetMovementApiUrl.GetUrl(AssetMovementEndpoint.Create), movement);
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                await PopulateAsync();
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
                await PopulateUsersAsync();
                await PopulateDepartmentsAsync();
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
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
                int? fromUserId = current.AssignedUserId;
                int? fromDepartmentId = current.AssignedDepartmentId;
                current.AssignedUserId = model.AssignedUserId;
                current.AssignedDepartmentId = model.AssignedDepartmentId;
                if (current.AssignedUserId != null || current.AssignedDepartmentId!=null)
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
                    ToLocationId = current.LocationId,
                    FromUserId = fromUserId,
                    ToUserId = model.AssignedUserId, // boşsa null olarak kalır
                    FromDepartmentId = fromDepartmentId,
                    ToDepartmentId = model.AssignedDepartmentId,
                    Note = "Varlık Zimmetlendi"
                };

                await _apiClientService.PostAsync<object>(
                    _assetMovementApiUrl.GetUrl(AssetMovementEndpoint.Create), movement);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                await PopulateUsersAsync();
                await PopulateDepartmentsAsync();
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
                await PopulateAsync();
                var models = await _apiClientService.GetAsync<List<ModelViewModel>>(_modelApiUrl.GetUrl(ModelEndpoint.GetAllActiveByBrandId, value.BrandId));
                ViewBag.Models = new SelectList(models, "Id", "ModelName");
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
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
                    ToLocationId = model.LocationId,
                    FromUserId = current.AssignedUserId,
                    ToUserId = model.AssignedUserId, // boşsa null olarak kalır
                    FromDepartmentId = current.AssignedDepartmentId,
                    ToDepartmentId = model.AssignedDepartmentId,
                    Note = "Varlık Düzenleme işlemi yapıldı."
                };

                await _apiClientService.PostAsync<object>(
                    _assetMovementApiUrl.GetUrl(AssetMovementEndpoint.Create), movement);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                await PopulateAsync();
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
                var current = await _apiClientService.GetAsync<UpdateAssetViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById, id));
                if (current == null)
                {
                    throw new Exception("Varlık bulunamadı");
                }
                if (!(current.Status == StatusEnum.Stokta || current.Status == StatusEnum.KullanimDisi))
                {
                    throw new Exception("Varlık durumu " + current.Status + " olduğu için pasif yapılamamaktadır.");
                }
                var result = await _apiClientService.DeleteAsync<object>(_AssetApiUrl.GetUrl(AssetEndpoint.DeActive, id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("SendToStock/{id}")]
        public async Task<IActionResult> SendToStock(int id)
        {
            try
            {
                var current = await _apiClientService.GetAsync<UpdateAssetViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById, id));
                if (current == null)
                {
                    throw new Exception("Varlık bulunamadı");
                }
                if (current.Status != StatusEnum.KullanimDisi)
                {
                    throw new Exception("Varlık durumu " + current.Status + " olduğu için stoğa çekilemez. Kullanım dışı olmalıdır.");
                }
                current.Status = StatusEnum.Stokta;
                var result = await _apiClientService.PutAsync<object>(_AssetApiUrl.GetUrl(AssetEndpoint.Update), current);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true);
            }
            return RedirectAfterPost(false);
        }
        [HttpGet("SendToDeactivate/{id}")]
        public async Task<IActionResult> SendToDeactivate(int id)
        {
            try
            {
                var current = await _apiClientService.GetAsync<UpdateAssetViewModel>(_AssetApiUrl.GetUrl(AssetEndpoint.GetById, id));
                if (current == null)
                {
                    throw new Exception("Varlık bulunamadı");
                }
                if (current.Status != StatusEnum.Stokta)
                {
                    throw new Exception("Varlık durumu " + current.Status + " olduğu için Kullanım Dışı yapılamaz. Önce durumunu stokta yapınız.");
                }
                current.Status = StatusEnum.KullanimDisi;
                var result = await _apiClientService.PutAsync<object>(_AssetApiUrl.GetUrl(AssetEndpoint.Update), current);
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
