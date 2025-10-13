using EnvanteriX.Application.Features.Commands.RoleCommands;
using EnvanteriX.Application.Features.Results.RoleResults;
using EnvanteriX.Application.Features.Rules.RoleRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EnvanteriX.Application.Features.Handlers.RoleHandlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateRoleCommandResult>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly RoleRules _roleRules;

        public CreateRoleCommandHandler(RoleManager<Role> roleManager, IMapper mapper, RoleRules roleRules)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _roleRules = roleRules;
        }

        public async Task<CreateRoleCommandResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole = await _roleManager.FindByNameAsync(request.Name);
            await _roleRules.RoleAlreadyExists(existingRole); // aynı isimde rol olmaması için kontrol
            var role =  _mapper.Map<Role, CreateRoleCommand>(request);
            role.NormalizedName = request.Name.ToUpper();
            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                throw new System.Exception("Role creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            var map = _mapper.Map<CreateRoleCommandResult, Role>(role);
            return map;
        }
    }
}
