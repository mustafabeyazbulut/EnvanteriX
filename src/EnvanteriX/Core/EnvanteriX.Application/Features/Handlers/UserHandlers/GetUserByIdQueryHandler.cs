using EnvanteriX.Application.Features.Queries.UserQueries;
using EnvanteriX.Application.Features.Results.UserResults;
using EnvanteriX.Application.Features.Rules.UserRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserRules _userRules;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(UserManager<User> userManager, UserRules userRules, IMapper mapper, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _userRules = userRules;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            await _userRules.UserShouldExist(user); // kullanıcı var mı kontrolü

            // Kullanıcının rolünü bul
            var roles = await _userManager.GetRolesAsync(user);
            var roleName = roles.FirstOrDefault();

            int roleId = 0;
            if (!string.IsNullOrEmpty(roleName))
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                    roleId = role.Id; // integer
            }

            // Mapping
            var map = _mapper.Map<GetUserByIdQueryResult, User>(user);
            map.RoleId = roleId;

            return map;
        }
    }
}

