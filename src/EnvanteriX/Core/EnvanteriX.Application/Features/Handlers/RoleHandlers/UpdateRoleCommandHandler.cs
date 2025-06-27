using EnvanteriX.Application.Features.Commands.RoleCommands;
using EnvanteriX.Application.Features.Rules.RoleRules;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.RoleHandlers
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Unit>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly RoleRules _roleRules;

        public UpdateRoleCommandHandler(RoleManager<Role> roleManager, RoleRules roleRules)
        {
            _roleManager = roleManager;
            _roleRules = roleRules;
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            await _roleRules.RoleShouldExistRule(role);

            if (!string.Equals(role.Name, request.Name, StringComparison.OrdinalIgnoreCase))
            {
                var existingRole = await _roleManager.FindByNameAsync(request.Name);
                await _roleRules.RoleAlreadyExists(existingRole); // aynı isimde rol olmaması için kontrol
            }

            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpper();

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                throw new System.Exception("Role update failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));

           return Unit.Value;
        }
    }
}
