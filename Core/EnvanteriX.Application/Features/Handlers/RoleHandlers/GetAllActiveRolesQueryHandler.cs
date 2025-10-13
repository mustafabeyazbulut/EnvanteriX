using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.RoleQueries;
using EnvanteriX.Application.Features.Results.RoleResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.RoleHandlers
{
    public class GetAllActiveRolesQueryHandler:BaseHandler,IRequestHandler<GetAllActiveRolesQuery, List<GetAllActiveRolesQueryResult>>
    {
        private readonly RoleManager<Role> _roleManager;

        public GetAllActiveRolesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, RoleManager<Role> roleManager) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _roleManager = roleManager;
        }

        public async Task<List<GetAllActiveRolesQueryResult>> Handle(GetAllActiveRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles
                                    .Where(r => !r.IsDeleted) 
                                    .ToList();
            var map = _mapper.Map<GetAllActiveRolesQueryResult, Role>(roles);
            return map.ToList();
        }
    }
}
