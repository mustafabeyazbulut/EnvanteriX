using EnvanteriX.Application.Features.Queries.UserQueries;
using EnvanteriX.Application.Features.Results.UserResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersQueryResult>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<GetAllUsersQueryResult>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result=await _userManager.Users.ToListAsync();
            var map = _mapper.Map<GetAllUsersQueryResult, User>(result);
            return map.ToList();
        }
    }
}
