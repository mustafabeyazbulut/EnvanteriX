using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AuthCommands;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.AuthHandlers
{
    public class RevokeAllCommandHandler : BaseHandler, IRequestHandler<RevokeAllCommand, Unit>
    {
        private readonly UserManager<User> userManager;
        public RevokeAllCommandHandler(UserManager<User> userManager,
            IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(RevokeAllCommand request, CancellationToken cancellationToken)
        {
            List<User> users = await userManager.Users.ToListAsync(cancellationToken); // tüm kullanıcıları alıyoruz
            foreach (User user in users) // tüm kullanıcılar için
            {
                user.RefreshToken = null;
                await userManager.UpdateAsync(user);
            }
            return Unit.Value;
        }
    }
}
