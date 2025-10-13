using EnvanteriX.Application.Features.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EnvanteriX.Domain.Entities;
using EnvanteriX.Application.Features.Rules.UserRules;
using EnvanteriX.Application.Interfaces.UnitOfWorks;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserRules _userRules;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(UserManager<User> userManager, UserRules userRules, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _userRules = userRules;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            await _userRules.UserShouldExist(user); //kullanıcı var mı yok mu kontrolü
            bool hasMovements = await _unitOfWork.GetReadRepository<AssetMovement>().AnyAsync(am =>am.FromUserId == request.UserId || am.ToUserId == request.UserId);
            await _userRules.UserHasAssetMovements(hasMovements); //kullanıcının hareketi var mı yok mu kontrolü
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to delete user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return Unit.Value;
        }
    }
}
