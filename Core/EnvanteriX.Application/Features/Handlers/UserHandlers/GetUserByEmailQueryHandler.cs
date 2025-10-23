using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.UserQueries;
using EnvanteriX.Application.Features.Results.UserResults;
using EnvanteriX.Application.Features.Rules.UserRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class GetUserByEmailQueryHandler : BaseHandler, IRequestHandler<GetUserByEmailQuery, GetUserByEmailQueryResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserRules _userRules;
        public GetUserByEmailQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, RoleManager<Role> roleManager, UserRules userRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRules = userRules;
        }

        public async Task<GetUserByEmailQueryResult> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email.ToString());
            await _userRules.UserShouldExist(user); // kullanıcı var mı kontrolü
            // Kullanıcının rolünü bul
            var roles = await _userManager.GetRolesAsync(user);
            var roleName = roles.FirstOrDefault();

            int roleId = 0;
            if (!string.IsNullOrEmpty(roleName))
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                
                if (role != null)
                {
                    roleId = role.Id; 
                    roleName = role.Name;
                }
            }

            // Mapping
            var map = _mapper.Map<GetUserByEmailQueryResult, User>(user);
            map.RoleId = roleId;
            map.Role = roleName;
            return map;
        }
    }
}
