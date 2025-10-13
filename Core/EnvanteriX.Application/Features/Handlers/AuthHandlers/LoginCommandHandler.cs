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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace EnvanteriX.Application.Features.Handlers.AuthHandlers
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommand, LoginCommandResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration configuration;
        private readonly ITokenService tokenService;
        private readonly AuthRules authRules;
        public LoginCommandHandler(UserManager<User> userManager, IConfiguration configuration, ITokenService tokenService, AuthRules authRules,
            IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            this._userManager = userManager;
            this.configuration = configuration;
            this.tokenService = tokenService;
            this.authRules = authRules;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //kod açıklamaları
            var user = await _userManager.Users
                            .Where(u => !u.IsDeleted && u.Email == request.Email)
                            .FirstOrDefaultAsync();
            await authRules.UserShouldExist(user); //kullanıcı yoksa hata fırlat
            await authRules.UserShouldBeActive(user); // kullanıcı aktif değilse hata fırlatıyoruz
            bool checkPassword = await _userManager.CheckPasswordAsync(user, request.Password); // kullanıcının şifresini kontrol ediyoruz

            await authRules.EmailOrPasswordShouldNotBeInvalid(user, checkPassword); // email veya şifre hatalı ise hata fırlatıyoruz

            IList<string> roles = await _userManager.GetRolesAsync(user); // kullanıcının rollerini alıyoruz


            JwtSecurityToken token = await tokenService.CreateToken(user, roles); // token oluşturuyoruz
            string refreshToken = tokenService.GenerateRefreshToken(); // refresh token oluşturuyoruz

            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays); // refresh token geçerlilik süresini alıyoruz

            user.RefreshToken = refreshToken; // kullanıcıya refresh token ekliyoruz
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays); // refresh token geçerlilik süresini ekliyoruz

            await _userManager.UpdateAsync(user); // kullanıcıyı güncelliyoruz
            await _userManager.UpdateSecurityStampAsync(user); // kullanıcı güvenlik damgasını güncelliyoruz

            string _token = new JwtSecurityTokenHandler().WriteToken(token); // tokeni yazıya çeviriyoruz

            await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token); // kullanıcıya token ekliyoruz

            return new()
            {
                Token = _token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };
        }
    }
}
