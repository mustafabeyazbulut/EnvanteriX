using EnvanteriX.Application.Features.Queries.RoleQueries;
using EnvanteriX.Application.Features.Results.RoleResults;
using EnvanteriX.Application.Features.Rules.RoleRules;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EnvanteriX.Application.Features.Handlers.RoleHandlers
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, GetRoleByIdQueryResult>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly RoleRules _roleRules;
        public GetRoleByIdQueryHandler(RoleManager<Role> roleManager, RoleRules roleRules)
        {
            _roleManager = roleManager;
            _roleRules = roleRules;
        }

        public async Task<GetRoleByIdQueryResult> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            await _roleRules.RoleShouldExistRule(role);

            return new GetRoleByIdQueryResult
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
        }
    }
}
