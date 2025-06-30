using EnvanteriX.Application.Features.Commands.UserCommands;
using EnvanteriX.Application.Features.Rules.UserRules;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserRules _userRules;

        public RemoveUserRoleCommandHandler(UserManager<User> userManager, UserRules userRules)
        {
            _userManager = userManager;
            _userRules = userRules;
        }

        public async Task<Unit> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            await _userRules.UserShouldExist(user); //kullanıcı var mı yok mu kontrolü

            var isInRole = await _userManager.IsInRoleAsync(user, request.RoleName);
            await _userRules.UserDoesNotHaveRoleException(isInRole, request.RoleName); //kullanıcı rolü var mı yok mu kontrolü

            var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Rol silinirken hata oluştu: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return Unit.Value;
        }
    }

}
