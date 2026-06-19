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
            var url = $"{_apiSettings.BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
            var response = await client.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
                catch (JsonException)
                {
                    throw new Exception($"API yanıtı çözümlenemedi ({response.StatusCode}): {responseContent}");
                }
            }
            else
            {
                try
                {
                    // API tarafındaki ExceptionModel'i çözümle
                    var errorModel = JsonConvert.DeserializeObject<ExceptionModel>(responseContent);
                    var errorMessage = string.Join(Environment.NewLine, errorModel.Errors);
                    throw new Exception($"API Hatası ({response.StatusCode}): {errorMessage}");
                }
                catch (JsonException)
                {
                    // JSON parse edilemezse ham metni göster
                    throw new Exception($"Status code: {response.StatusCode}{Environment.NewLine}{responseContent}");
                }
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var client = await GetHttpClientWithTokenAsync();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var url = $"{_apiSettings.BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
            var response = await client.PostAsync(url, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
                catch (JsonException)
                {
                    throw new Exception($"API yanıtı çözümlenemedi ({response.StatusCode}): {responseContent}");
                }
            }
            else
            {
                try
                {
                    // API'den dönen hata yapısını (ExceptionModel) çözümle
                    var errorModel = JsonConvert.DeserializeObject<ExceptionModel>(responseContent);
                    var errorMessage = string.Join(Environment.NewLine, errorModel.Errors);
                    throw new Exception($"API Hatası ({response.StatusCode}): {errorMessage}");
                }
                catch (JsonException)
                {
                    // JSON parse edilemezse ham metni göster
                    throw new Exception($"Status code: {response.StatusCode}{Environment.NewLine}{responseContent}");
                }
            }
        }

        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            var client = await GetHttpClientWithTokenAsync();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var url = $"{_apiSettings.BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
            var response = await client.PutAsync(url, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
                catch (JsonException)
                {
                    throw new Exception($"API yanıtı çözümlenemedi ({response.StatusCode}): {responseContent}");
                }
            }
            else
            {
                try
                {
                    // API'nin ExceptionModel yapısına göre deserialize et
                    var errorModel = JsonConvert.DeserializeObject<ExceptionModel>(responseContent);
                    var errorMessage = string.Join(Environment.NewLine, errorModel.Errors);
                    throw new Exception($"API Hatası ({response.StatusCode}): {errorMessage}");
                }
                catch (JsonException)
                {
                    // JSON parse edilemiyorsa ham içerik ile hata fırlat
                    throw new Exception($"Status code: {response.StatusCode}{Environment.NewLine}{responseContent}");
                }
            }
        }

        public async Task<T> DeleteAsync<T>(string endpoint)
        {
            var client = await GetHttpClientWithTokenAsync();
            var url = $"{_apiSettings.BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
            var response = await client.DeleteAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
                catch (JsonException)
                {
                    throw new Exception($"API yanıtı çözümlenemedi ({response.StatusCode}): {responseContent}");
                }
            }
            else
            {
                // API'den dönen hatayı yakala ve içeriği fırlat
                try
                {
                    var errorModel = JsonConvert.DeserializeObject<ExceptionModel>(responseContent);
                    var errorMessage = string.Join(Environment.NewLine, errorModel.Errors);
                    throw new Exception($"API Hatası ({response.StatusCode}): {errorMessage}");
                }
                catch (JsonException)
                {
                    // JSON çözümlenemiyorsa ham mesajı at
                    throw new Exception($"Status code: {response.StatusCode}{Environment.NewLine}{responseContent}");
                }
            }
        }
        public class ExceptionModel
        {
            public IEnumerable<string> Errors { get; set; }
            public int Status { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }


    }
}
