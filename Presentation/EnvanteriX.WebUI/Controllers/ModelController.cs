using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Brand;
using EnvanteriX.WebUI.ViewModels.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("Model")]
    public class ModelController : BaseController
    {
        private readonly ModelApiUrl _ModelApiUrl;
        private readonly BrandApiUrl _brandApiUrl;
        public ModelController(IApiClientService apiClientService, ModelApiUrl ModelApiUrl, BrandApiUrl brandApiUrl) : base(apiClientService)
        {
            _ModelApiUrl = ModelApiUrl;
            _brandApiUrl = brandApiUrl;
        }
        private void PopulateBrands()
        {
            var brands = _apiClientService.GetAsync<List<BrandViewModel>>(_brandApiUrl.GetUrl(BrandEndpoint.GetAllActive)).Result;
            ViewBag.Brands = new SelectList(brands, "Id", "BrandName");
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<ModelViewModel>>(_ModelApiUrl.GetUrl(ModelEndpoint.GetAll));
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<ModelViewModel>());
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
                PopulateBrands();
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateModelViewModel model)
        {
            try
            {
                var result = await _apiClientService.PostAsync<object>(_ModelApiUrl.GetUrl(ModelEndpoint.Create), model);
                PopulateBrands();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true,model);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateModelViewModel>(_ModelApiUrl.GetUrl(ModelEndpoint.GetById, id));
                PopulateBrands();
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
        public async Task<IActionResult> Edit(UpdateModelViewModel model)
        {
            try
            {
                var result = await _apiClientService.PutAsync<object>(_ModelApiUrl.GetUrl(ModelEndpoint.Update), model);
                PopulateBrands();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true, model);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_ModelApiUrl.GetUrl(ModelEndpoint.Delete, id));
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
                var result = await _apiClientService.DeleteAsync<object>(_ModelApiUrl.GetUrl(ModelEndpoint.Active, id));
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
                var result = await _apiClientService.DeleteAsync<object>(_ModelApiUrl.GetUrl(ModelEndpoint.DeActive, id));
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
