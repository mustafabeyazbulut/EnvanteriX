using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AuthCommands;
using EnvanteriX.Application.Features.Rules.AuthRules;
using EnvanteriX.Application.Features.Rules.RoleRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.AuthHandlers
{
    public class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommand, Unit>
    {
        private readonly AuthRules authRules;
        private readonly RoleRules _roleRules;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        public RegisterCommandHandler(AuthRules authRules, UserManager<User> userManager, RoleManager<Role> roleManager,
            IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, RoleRules roleRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            this.authRules = authRules;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _roleRules = roleRules;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await authRules.UserShouldNotBeExist(await userManager.FindByEmailAsync(request.Email));// kullanıcı var mı kontrol ediyoruz

            User user = _mapper.Map<User, RegisterCommand>(request); // kullanıcıyı map ediyoruz
            user.UserName = request.Email.Split("@")[0];
            user.NormalizedUserName = request.Email.Split("@")[0];
            user.NormalizedEmail = request.Email;
            user.SecurityStamp = Guid.NewGuid().ToString(); // güvenlik damgası ekliyoruz

            var role = await roleManager.FindByNameAsync(request.Role); // rolü buluyoruz
            await _roleRules.RoleShouldExistRule(role); //rol yoksa hata fırlatıyoruz.

            IdentityResult result = await userManager.CreateAsync(user, request.Password); // kullanıcıyı oluşturuyoruz
            if (result.Succeeded) // kullanıcı oluşturulduysa
            {
                await userManager.AddToRoleAsync(user, role.Name);
            }
            return Unit.Value;
        }
    }
}
