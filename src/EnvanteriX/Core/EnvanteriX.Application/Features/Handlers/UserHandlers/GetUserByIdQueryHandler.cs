using EnvanteriX.Application.Features.Queries.UserQueries;
using EnvanteriX.Application.Features.Results.UserResults;
using EnvanteriX.Application.Features.Rules.UserRules;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserRules _userRules;

        public GetUserByIdQueryHandler(UserManager<User> userManager, UserRules userRules)
        {
            _userManager = userManager;
            _userRules = userRules;
        }

        public async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            await _userRules.UserShouldExist(user); //kullanıcı var mı yok mu kontrolü

            return new GetUserByIdQueryResult
            {
                UserId = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive
            };
        }
    }
}
