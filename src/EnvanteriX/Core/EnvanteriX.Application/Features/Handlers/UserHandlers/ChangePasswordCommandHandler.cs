using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.UserCommands;
using EnvanteriX.Application.Features.Rules.UserRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class ChangePasswordCommandHandler : BaseHandler, IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserRules _userRules;
        public ChangePasswordCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, UserRules userRules) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _userManager = userManager;
            _userRules = userRules;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await _userRules.PasswordsShouldMatch(request.Password, request.ConfirmPassword);
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            await _userRules.UserShouldExist(user); //kullanıcı var mı yok mu kontrolü
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new System.Exception(string.Join(", ", result.Errors));
            return Unit.Value;
        }
    }
}
