using EnvanteriX.Application.Features.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EnvanteriX.Domain.Entities;
using EnvanteriX.Application.Features.Rules.UserRules;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserRules _userRules;

        public DeleteUserCommandHandler(UserManager<User> userManager, UserRules userRules)
        {
            _userManager = userManager;
            _userRules = userRules;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            await _userRules.UserShouldExist(user); //kullanıcı var mı yok mu kontrolü
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to delete user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return Unit.Value;
        }
    }
}
