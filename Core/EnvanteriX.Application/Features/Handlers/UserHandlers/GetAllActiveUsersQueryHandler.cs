using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.UserQueries;
using EnvanteriX.Application.Features.Results.UserResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class GetAllActiveUsersQueryHandler : BaseHandler, IRequestHandler<GetAllActiveUsersQuery, List<GetAllActiveUsersQueryResult>>
    {
        private readonly UserManager<User> _userManager;
        public GetAllActiveUsersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _userManager = userManager;
        }

        public async Task<List<GetAllActiveUsersQueryResult>> Handle(GetAllActiveUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Where(u => !u.IsDeleted).ToListAsync(cancellationToken);
            var userResults = new List<GetAllActiveUsersQueryResult>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var roleName = roles.FirstOrDefault(); // sadece ilk (tek) rol alınır

                var mappedUser = _mapper.Map<GetAllActiveUsersQueryResult, User>(user);

                mappedUser.Role = roleName; // result modeline rol adı yazılır

                userResults.Add(mappedUser);
            }

            return userResults;
        }
    }
}
