using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AuthCommands;
using EnvanteriX.Application.Features.Results.AuthResults;
using EnvanteriX.Application.Features.Rules.AuthRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.Tokens;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EnvanteriX.Application.Features.Handlers.AuthHandlers
{
    public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResult>
    {
        private readonly AuthRules authRules;
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        public RefreshTokenCommandHandler(AuthRules authRules, UserManager<User> userManager, ITokenService tokenService,
            IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            this.authRules = authRules;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<RefreshTokenCommandResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal? principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken); // tokeni parse ediyoruz
            string email = principal.FindFirstValue(ClaimTypes.Email); // email adresini alıyoruz

            User? user = await userManager.FindByEmailAsync(email); // kullanıcıyı email adresine göre buluyoruz
            await authRules.UserShouldExist(user); //kullanıcı yoksa hata fırlat
            IList<string> roles = await userManager.GetRolesAsync(user); // kullanıcının rollerini alıyoruz

            await authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpiryTime); // refresh token süresi geçmiş ise hata fırlatıyoruz

            JwtSecurityToken newAccessToken = await tokenService.CreateToken(user, roles); // yeni token oluşturuyoruz
            string newRefreshToken = tokenService.GenerateRefreshToken(); // yeni refresh token oluşturuyoruz

            user.RefreshToken = newRefreshToken;
            await userManager.UpdateAsync(user);
            var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
            return new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                Expiration = newAccessToken.ValidTo
            };
        }
    }
}
