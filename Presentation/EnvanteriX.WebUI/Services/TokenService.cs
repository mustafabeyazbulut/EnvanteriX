using EnvanteriX.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnvanteriX.WebUI.Services
{
    public class TokenService : ITokenService
    {
        private readonly ApiSettings _apiSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _apiSettings = apiSettings.Value;
        }
        public async Task<TokenResponse> LoginAsync(string email, string password, bool rememberMe)
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            var client = _httpClientFactory.CreateClient();
            var loginData = new { email, password };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_apiSettings.BaseUrl+ _apiSettings.LoginUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenResponse.token);

                // Payload içindeki "role" claim'ini al
                // Payload içindeki rol claim'ini bul
                var roles = token.Claims
                        .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                        .Select(c => c.Value)
                        .ToList();
                
                // Sign in the user
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, email), // You can add other claims if needed
                        new Claim("AccessToken", tokenResponse.token),
                        new Claim("RefreshToken", tokenResponse.refreshToken),
                    };
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                // Set the cookie expiration time
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),  // Cookie'nin bitiş zamanı
                    IsPersistent = rememberMe  // Cookie'nin sürekli olmaması gerektiğini belirtir
                };
                // Issue the authentication cookie
                await _httpContextAccessor.HttpContext.SignInAsync("Cookies", claimsPrincipal, authProperties);
                return tokenResponse;
            }
            else
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                if (errorResponse.status==400)
                {
                    throw new Exception($"Status code: {errorResponse.status} || {string.Join("\n", errorResponse.errors.Select(e => $"{e.Key}: {string.Join(", ", e.Value)}"))}");
                }
                else
                {
                    throw new Exception($"Status code: {errorResponse.status} || {string.Join("\n", errorResponse.Errors[0])}");
                }

            }
        }

        // AccessToken'ı al
        public async Task<string> GetAccessTokenAsync()
        {
            var accessToken = _httpContextAccessor.HttpContext.User.FindFirst("AccessToken")?.Value;
            return accessToken;
        }

        // Token'ın süresinin dolup dolmadığını kontrol et
        public async Task<bool> IsTokenExpired()
        {
            var accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken))
                return true;

            // Token'ın süresini kontrol et (JWT'den veya başka bir kaynaktan)
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            var expirationDate = jwtToken.ValidTo;
            return expirationDate <= DateTime.UtcNow;
        }

        // Token'ı yenile
        public async Task<string> RefreshTokenAsync()
        {
            //Task.Run(() => _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)).Wait();
            // Check if HttpContext is available
            if (_httpContextAccessor.HttpContext == null)
            {
                throw new Exception("HttpContext is null.");
            }

            var currentClaimsPrincipal = _httpContextAccessor.HttpContext.User;

            var refreshToken = _httpContextAccessor.HttpContext.User.FindFirst("RefreshToken")?.Value;

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new Exception("Refresh token bulunamadı.");
            }

            var client = _httpClientFactory.CreateClient();
            var refreshTokenRequest = new
            {
                RefreshToken = refreshToken,
                AccessToken = _httpContextAccessor.HttpContext.User.FindFirst("AccessToken")?.Value
            };

            var content = new StringContent(JsonConvert.SerializeObject(refreshTokenRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_apiSettings.BaseUrl + _apiSettings.RefreshTokenUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Refresh token yenileme işlemi başarısız.");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseData);

            // Update claims
            var claims = currentClaimsPrincipal.Claims.ToList();
            claims.RemoveAll(c => c.Type == "AccessToken" || c.Type == "RefreshToken");
            // Add new token and refresh token
            claims.Add(new Claim("AccessToken", tokenResponse.token));
            claims.Add(new Claim("RefreshToken", tokenResponse.refreshToken));

            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Update authentication cookie
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),  // Cookie expiration time
                IsPersistent = true  // Make the cookie persistent
            };
            //await _httpContextAccessor.HttpContext.SignOutAsync("Cookies");
             //ClearAllCookies(_httpContextAccessor.HttpContext);
            // Sign the user in again after refreshing the token
            await _httpContextAccessor.HttpContext.SignInAsync("Cookies", claimsPrincipal, authProperties);

            return tokenResponse.token;
        }
        public void ClearAllCookies(HttpContext httpContext)
        {
            foreach (var cookie in httpContext.Request.Cookies.Keys)
            {
                httpContext.Response.Cookies.Delete(cookie);
            }
           
        }
    }
}
