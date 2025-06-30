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

            if (!string.Equals(user.UserName, request.UserName, StringComparison.OrdinalIgnoreCase))
            {
                var existingRole = await _userManager.FindByNameAsync(request.UserName);
                await _userRules.UserAlreadyExists(existingRole); // aynı isimde rol olmaması için kontrol
            }
            if (!string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase))
            {
                var existingRole = await _userManager.FindByEmailAsync(request.Email);
                await _userRules.UserAlreadyExists(existingRole); // aynı isimde rol olmaması için kontrol
            }
            user.FullName = request.FullName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new System.Exception(string.Join(", ", result.Errors));

            return Unit.Value;
        }
    }
}
