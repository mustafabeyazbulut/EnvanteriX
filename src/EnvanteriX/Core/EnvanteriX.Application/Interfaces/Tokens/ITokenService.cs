using EnvanteriX.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EnvanteriX.Application.Interfaces.Tokens
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(User user, IList<string> roles);
        string GenerateRefreshToken();

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        // Bu fonksiyon token'ı parse edip içindeki bilgileri alıp bir principal oluşturuyor.
        // Yani token'ı parse edip içindeki bilgileri almak için kullanılıyor.

    }
}
