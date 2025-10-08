using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.RoleQueries;
using EnvanteriX.Application.Features.Results.RoleResults;
using EnvanteriX.Application.Features.Rules.RoleRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.RoleHandlers
{
    public class GetRoleByIdQueryHandler :BaseHandler, IRequestHandler<GetRoleByIdQuery, GetRoleByIdQueryResult>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly RoleRules _roleRules;

        public GetRoleByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, RoleManager<Role> roleManager, RoleRules roleRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _roleManager = roleManager;
            _roleRules = roleRules;
        }

        public async Task<GetRoleByIdQueryResult> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            await _roleRules.RoleShouldExistRule(role);
            return _mapper.Map<GetRoleByIdQueryResult, Role>(role);
        }
    }
}
