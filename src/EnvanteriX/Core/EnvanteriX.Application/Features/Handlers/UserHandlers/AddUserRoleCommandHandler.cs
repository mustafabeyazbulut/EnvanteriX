using EnvanteriX.Application.Features.Commands.UserCommands;
using EnvanteriX.Application.Features.Rules.RoleRules;
using EnvanteriX.Application.Features.Rules.UserRules;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class AddUserRoleCommandHandler : IRequestHandler<AddUserRoleCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserRules _userRules;
        private readonly RoleManager<Role> _roleManager;
        private readonly RoleRules _roleRules;

        public AddUserRoleCommandHandler(UserManager<User> userManager, UserRules userRules, RoleManager<Role> roleManager, RoleRules roleRules)
        {
            _userManager = userManager;
            _userRules = userRules;
            _roleManager = roleManager;
            _roleRules = roleRules;
        }

        public async Task<Unit> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            await _userRules.UserShouldExist(user); //kullanıcı var mı yok mu kontrolü

            var isRole = await _roleManager.FindByNameAsync(request.RoleName);
            await _roleRules.RoleShouldExistRule(isRole);

            var isInRole = await _userManager.IsInRoleAsync(user, request.RoleName);
            if (isInRole)
            {
                throw new Exception($"{request.RoleName} isimli Rol zaten kullanıcıya tanımlıdır");
            }
            var result = await _userManager.AddToRoleAsync(user, request.RoleName);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Rol eklenirken hata oluştu: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return Unit.Value;
        }
    }
}
