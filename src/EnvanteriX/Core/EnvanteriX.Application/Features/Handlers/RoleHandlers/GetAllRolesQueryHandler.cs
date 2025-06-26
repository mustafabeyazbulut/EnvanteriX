using EnvanteriX.Application.Features.Queries.RoleQueries;
using EnvanteriX.Application.Features.Results.RoleResults;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.RoleHandlers
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<GetAllRolesQueryResult>>
    {
        private readonly RoleManager<Role> _roleManager;

        public GetAllRolesQueryHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<GetAllRolesQueryResult>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles.ToList();

            return roles.Select(r => new GetAllRolesQueryResult
            {
                RoleId = r.Id,
                RoleName = r.Name
            }).ToList();
        }
    }
}
