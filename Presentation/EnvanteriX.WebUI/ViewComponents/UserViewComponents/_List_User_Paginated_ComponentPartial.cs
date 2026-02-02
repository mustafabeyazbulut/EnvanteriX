using EnvanteriX.WebUI.Models.ApiUrl;
using EnvanteriX.WebUI.Services;
using EnvanteriX.WebUI.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.UserViewComponents
{
    public class _List_User_Paginated_ComponentPartial : ViewComponent
    {
        private readonly IApiClientService _apiClientService;
        private readonly UserApiUrl _userApiUrl;

        public _List_User_Paginated_ComponentPartial(
            IApiClientService apiClientService,
            UserApiUrl userApiUrl)
        {
            _apiClientService = apiClientService;
            _userApiUrl = userApiUrl;
        }

        public async Task<IViewComponentResult> InvokeAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? role = null,
            bool? isDeleted = null)
        {
            try
            {
                var queryParams = new List<string>
                {
                    $"pageNumber={pageNumber}",
                    $"pageSize={pageSize}"
                };

                if (!string.IsNullOrWhiteSpace(searchTerm))
                    queryParams.Add($"searchTerm={Uri.EscapeDataString(searchTerm)}");

                if (!string.IsNullOrWhiteSpace(role))
                    queryParams.Add($"role={Uri.EscapeDataString(role)}");

                if (isDeleted.HasValue)
                    queryParams.Add($"isDeleted={isDeleted.Value}");

                var queryString = string.Join("&", queryParams);
                var url = $"{_userApiUrl.GetUrl(UserEndpoint.GetAllPaginated)}?{queryString}";

                var paginatedResult = await _apiClientService.GetAsync<PaginatedUserViewModel>(url);

                return View(paginatedResult ?? new PaginatedUserViewModel());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Kullanıcılar yüklenirken hata oluştu: {ex.Message}";
                return View(new PaginatedUserViewModel());
            }
        }
    }
}
