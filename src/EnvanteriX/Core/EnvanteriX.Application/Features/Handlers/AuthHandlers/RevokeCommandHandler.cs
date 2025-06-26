using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AuthCommands;
using EnvanteriX.Application.Features.Rules.AuthRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.AuthHandlers
{
    public class RevokeCommandHandler : BaseHandler, IRequestHandler<RevokeCommand, Unit>
    {
        private readonly UserManager<User> userManager;
        private readonly AuthRules authRules;
        public RevokeCommandHandler(UserManager<User> userManager, AuthRules authRules,
            IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            this.userManager = userManager;
            this.authRules = authRules;
        }

        public async Task<Unit> Handle(RevokeCommand request, CancellationToken cancellationToken)
        {
            User user = await userManager.FindByEmailAsync(request.Email); // kullanıcıyı email adresine göre buluyoruz
            await authRules.EmailAddressShouldBeValid(user); // email adresi hatalı ise hata fırlatıyoruz

            user.RefreshToken = null; // kullanıcı refresh token'ını siliyoruz
            await userManager.UpdateAsync(user); // kullanıcıyı güncelliyoruz

            return Unit.Value;
        }
    }
}
