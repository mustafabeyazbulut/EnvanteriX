using EnvanteriX.Application.Features.Commands.RoleCommands;
using EnvanteriX.Application.Features.Results.RoleResults;
using EnvanteriX.Application.Features.Rules.RoleRules;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.RoleHandlers
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleCommandResult>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly RoleRules _roleRules;

        public UpdateRoleCommandHandler(RoleManager<Role> roleManager, RoleRules roleRules)
        {
            _roleManager = roleManager;
            _roleRules = roleRules;
        }

        public async Task<UpdateRoleCommandResult> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            await _roleRules.RoleShouldExistRule(role);

            role.Name = request.RoleName;
            role.NormalizedName = request.RoleName.ToUpper();

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                throw new System.Exception("Role update failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            return new UpdateRoleCommandResult
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
        }
    }
}
