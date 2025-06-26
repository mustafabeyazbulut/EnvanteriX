using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.Controllers
{
    [Route("Company")]
    public class CompanyController : BaseController
    {
        private readonly CompanyApiUrl _companyApiUrl;

        public CompanyController(IApiClientService apiClientService, CompanyApiUrl companyApiUrl) : base(apiClientService)
        {
            _companyApiUrl = companyApiUrl;
        }

        [Authorize(Roles = "user")]
        [HttpGet("")]
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                var values = await _apiClientService.GetAsync<List<CompanyViewModel>>(_companyApiUrl.CompanyList);
                return View(values.OrderByDescending(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View(new List<CompanyViewModel>());
        }

        [Authorize(Roles = "user")]
        [HttpGet("Add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateCompanyViewModel model)
        {
            try
            {
                var result = await _apiClientService.PostAsync<object>(_companyApiUrl.CreateCompany, model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            }
            return RedirectToAction("List","Company");
        }

        [Authorize(Roles = "user")]
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateCompanyViewModel>(_companyApiUrl.GetCompany + id);
                return View(value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("List", "Company");
        }
        [Authorize(Roles = "user")]
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(UpdateCompanyViewModel model)
        {
            try
            {
                var result = await _apiClientService.PutAsync<object>(_companyApiUrl.UpdateCompany, model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("List", "Company");
        }

        [Authorize(Roles = "user")]
        [HttpGet("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _apiClientService.DeleteAsync<object>(_companyApiUrl.RemoveCompany, id);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("List", "Company");
        }

        [Authorize(Roles = "user")]
        [HttpGet("Active/{id}")]
        public async Task<IActionResult> Active(int id)
        {
            try
            {
                var value = await _apiClientService.GetAsync<UpdateCompanyViewModel>(_companyApiUrl.GetCompany + id);
                value.IsDeleted = false;
                var result = await _apiClientService.PutAsync<object>(_companyApiUrl.UpdateCompany, value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return RedirectToAction("List", "Company");
        }
    }
}
