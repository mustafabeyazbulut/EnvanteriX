using EnvanteriX.Application.Features.Commands.RoleCommands;
using EnvanteriX.Application.Features.Results.RoleResults;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EnvanteriX.Application.Features.Handlers.RoleHandlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateRoleCommandResult>
    {
        private readonly RoleManager<Role> _roleManager;

        public CreateRoleCommandHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<CreateRoleCommandResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role { Name = request.RoleName, NormalizedName = request.RoleName.ToUpper(),ConcurrencyStamp= Guid.NewGuid().ToString() };
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                throw new System.Exception("Role creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            return new CreateRoleCommandResult
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
        }
    }
}
