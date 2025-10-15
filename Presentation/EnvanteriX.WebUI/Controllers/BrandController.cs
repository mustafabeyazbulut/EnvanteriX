using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Brand;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("Brand")]
    public class BrandController : BaseController
    {
        private readonly BrandApiUrl _BrandApiUrl;
        public BrandController(IApiClientService apiClientService, BrandApiUrl BrandApiUrl) : base(apiClientService)
        {
            _BrandApiUrl = BrandApiUrl;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<BrandViewModel>>(_BrandApiUrl.GetUrl(BrandEndpoint.GetAll));
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<BrandViewModel>());
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateBrandViewModel model)
        {
            try
            {
                var result = await _apiClientService.PostAsync<object>(_BrandApiUrl.GetUrl(BrandEndpoint.Create), model);
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
                var value = await _apiClientService.GetAsync<UpdateBrandViewModel>(_BrandApiUrl.GetUrl(BrandEndpoint.GetById, id));
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
        public async Task<IActionResult> Edit(UpdateBrandViewModel model)
        {
            try
            {
                var result = await _apiClientService.PutAsync<object>(_BrandApiUrl.GetUrl(BrandEndpoint.Update), model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
                return RedirectAfterPost(true,model);
            }
            return RedirectAfterPost(false);
        }

        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_BrandApiUrl.GetUrl(BrandEndpoint.Delete, id));
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
                var result = await _apiClientService.DeleteAsync<object>(_BrandApiUrl.GetUrl(BrandEndpoint.Active, id));
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
                var result = await _apiClientService.DeleteAsync<object>(_BrandApiUrl.GetUrl(BrandEndpoint.DeActive, id));
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
