using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("Category")]
    public class CategoryController : BaseController
    {
        private readonly CategoryApiUrl _categoryApiUrl;

        public CategoryController(IApiClientService apiClientService, CategoryApiUrl categoryApiUrl) : base(apiClientService)
        {
            _categoryApiUrl = categoryApiUrl;
        }

        [Authorize(Roles = "user")]
        [HttpGet("")]
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<CategoryViewModel>>(_categoryApiUrl.CategoryList);
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<CategoryViewModel>());
        }

        [Authorize(Roles = "user")]
        [HttpGet("Add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateCategoryViewModel model)
        {
            try
            {
                var result = await _apiClientService.PostAsync<object>(_categoryApiUrl.CreateCategory, model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            }
            return RedirectToAction("List", "Category");
        }

        [Authorize(Roles = "user")]
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateCategoryViewModel>(_categoryApiUrl.GetCategory + id);
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("List", "Category");
        }
        [Authorize(Roles = "user")]
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateCategoryViewModel model)
        {
            try
            {
                var result = await _apiClientService.PutAsync<object>(_categoryApiUrl.UpdateCategory, model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("List", "Category");
        }

        [Authorize(Roles = "user")]
        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_categoryApiUrl.RemoveCategory, id);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("List", "Category");
        }

        [Authorize(Roles = "user")]
        [HttpGet("Active/{id}")]
        public async Task<IActionResult> Active(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateCategoryViewModel>(_categoryApiUrl.GetCategory + id);
                value.IsDeleted = false;
                var result = await _apiClientService.PutAsync<object>(_categoryApiUrl.UpdateCategory, value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("List", "Category");
        }
    }
}
