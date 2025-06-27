using EnvanteriX.Application.Features.Commands.UserCommands;
using EnvanteriX.Application.Features.Rules.UserRules;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserRules _userRules;

        public UpdateUserCommandHandler(UserManager<User> userManager, UserRules userRules)
        {
            _userManager = userManager;
            _userRules = userRules;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            await _userRules.UserShouldExist(user); //kullanıcı var mı yok mu kontrolü

            user.FullName = request.FullName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.IsActive = request.IsActive;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new System.Exception(string.Join(", ", result.Errors));

            return Unit.Value;
        }
    }
}
