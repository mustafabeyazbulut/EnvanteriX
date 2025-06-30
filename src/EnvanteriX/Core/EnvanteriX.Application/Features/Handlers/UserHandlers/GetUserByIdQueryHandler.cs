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
        private readonly UserRules _userRules;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(UserManager<User> userManager, UserRules userRules, IMapper mapper)
        {
            _userManager = userManager;
            _userRules = userRules;
            _mapper = mapper;
        }

        public async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            await _userRules.UserShouldExist(user); //kullanıcı var mı yok mu kontrolü
            var map = _mapper.Map<GetUserByIdQueryResult, User>(user);
            return map;
        }
    }
}
