using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.PasswordGenerator;
using EnvanteriX.Application.Interfaces.Portal365Interfaces;
using EnvanteriX.Application.Interfaces.Tokens;
using EnvanteriX.Infrastructure.Portal365Services;
using EnvanteriX.Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EnvanteriX.Infrastructure.PasswordGenerators;

namespace EnvanteriX.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
            services.AddTransient<ITokenService, TokenService>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    ValidateLifetime = false,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero,
                };
            });
            services.AddSingleton<IMapper, AutoMapper.Mapper>();
            services.AddHttpClient<IPortal365Service, Portal365Service>();
            services.AddSingleton<IPasswordGenerator, PasswordGenerator>();


        }
    }
}
