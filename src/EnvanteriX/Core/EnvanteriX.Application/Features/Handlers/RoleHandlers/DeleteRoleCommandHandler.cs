using EnvanteriX.Application.Features.Commands.RoleCommands;
using EnvanteriX.Application.Features.Rules.RoleRules;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.RoleHandlers
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly RoleRules _roleRules;

        public DeleteRoleCommandHandler(RoleManager<Role> roleManager, RoleRules roleRules)
        {
            _roleManager = roleManager;
            _roleRules = roleRules;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            await _roleRules.RoleShouldExistRule(role);

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to delete role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return Unit.Value;
        }
    }
}
