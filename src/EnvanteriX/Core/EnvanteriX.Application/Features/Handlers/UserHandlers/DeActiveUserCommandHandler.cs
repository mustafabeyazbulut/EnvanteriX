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
    public class DeActiveUserCommandHandler : BaseHandler, IRequestHandler<DeActiveUserCommand, Unit>
    {
        private readonly UserRules _userRules;
        private readonly UserManager<User> _userManager;
        public DeActiveUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserRules userRules, UserManager<User> userManager)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _userRules = userRules;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(DeActiveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            await _userRules.UserShouldExist(user); //kullanıcı var mı yok mu kontrolü
            user.IsDeleted = false;
            await _userManager.UpdateAsync(user);
            return Unit.Value;
        }
    }
}
