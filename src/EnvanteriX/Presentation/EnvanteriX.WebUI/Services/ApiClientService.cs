using EnvanteriX.WebUI.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EnvanteriX.WebUI.Services
{
    public class ApiClientService : IApiClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        private readonly ApiSettings _apiSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApiClientService(IHttpClientFactory httpClientFactory,
            ITokenService tokenService,
            IOptions<ApiSettings> apiSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
            _apiSettings = apiSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        private async Task<HttpClient> GetHttpClientWithTokenAsync()
        {
            var accessToken = _httpContextAccessor.HttpContext.User.FindFirst("AccessToken")?.Value;

            // Eğer access token yoksa veya geçersizse, yenileyin
            if (string.IsNullOrEmpty(accessToken) || await _tokenService.IsTokenExpired())
            {
                accessToken = await _tokenService.RefreshTokenAsync();
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return client;
        }
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var client = await GetHttpClientWithTokenAsync();
            var response = await client.GetAsync(_apiSettings.BaseUrl + endpoint);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            else
            {
                throw new Exception($"Status code: {response.StatusCode} {Environment.NewLine} {string.Join("\n", response.RequestMessage.RequestUri)}{Environment.NewLine} {response.ReasonPhrase}");

            }
        }
        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var client = await GetHttpClientWithTokenAsync();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_apiSettings.BaseUrl + endpoint, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            else
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                if (errorResponse.status == 400)
                {
                    throw new Exception($"Status code: {errorResponse.status} {Environment.NewLine} {string.Join("\n", errorResponse.errors.Select(e => $"{e.Key}: {string.Join(", ", e.Value)}"))}");
                }
                else
                {
                    throw new Exception($"Status code: {errorResponse.status} {Environment.NewLine} {string.Join("\n", errorResponse.Errors[0])}");
                }
            }
        }
        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            var client = await GetHttpClientWithTokenAsync();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(_apiSettings.BaseUrl + endpoint, content);

            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            else
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                if (errorResponse.status == 400)
                {
                    throw new Exception($"Status code: {errorResponse.status} {Environment.NewLine} {string.Join("\n", errorResponse.errors.Select(e => $"{e.Key}: {string.Join(", ", e.Value)}"))}");
                }
                else
                {
                    throw new Exception($"Status code: {errorResponse.status} {Environment.NewLine} {string.Join("\n", errorResponse.Errors[0])}");
                }
            }
        }
        public async Task<T> DeleteAsync<T>(string endpoint, int data)
        {
            var client = await GetHttpClientWithTokenAsync();
            var url = $"{_apiSettings.BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}/{data}";
            var response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            else
            {
                throw new Exception($"Status code: {response.StatusCode} {Environment.NewLine} {string.Join("\n", response.RequestMessage.RequestUri)}{Environment.NewLine} {response.ReasonPhrase}");
            }
        }
    }
}
