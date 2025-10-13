using EnvanteriX.WebUI.Models;

namespace EnvanteriX.WebUI.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> LoginAsync(string username, string password, bool rememberMe);
        Task<string> GetAccessTokenAsync();
        Task<bool> IsTokenExpired();
        Task<string> RefreshTokenAsync();
    }
}
