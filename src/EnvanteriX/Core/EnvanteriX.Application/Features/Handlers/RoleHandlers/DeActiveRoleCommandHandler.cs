using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.RoleCommands;
using EnvanteriX.Application.Features.Rules.RoleRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Handlers.RoleHandlers
{
    public class DeActiveRoleCommandHandler : BaseHandler, IRequestHandler<DeActiveRoleCommand, Unit>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly RoleRules _roleRules;
        public DeActiveRoleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeActiveRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            await _roleRules.RoleShouldExistRule(role);
            role.IsDeleted = true;

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to deactivate role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return Unit.Value;
        }
    }
}
