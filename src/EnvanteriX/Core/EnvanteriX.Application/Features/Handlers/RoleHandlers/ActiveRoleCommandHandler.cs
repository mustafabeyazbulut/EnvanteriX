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
    public class ActiveRoleCommandHandler : BaseHandler, IRequestHandler<ActiveRoleCommand, Unit>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly RoleRules _roleRules;
        public ActiveRoleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, RoleRules roleRules, RoleManager<Role> roleManager) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _roleRules = roleRules;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(ActiveRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            await _roleRules.RoleShouldExistRule(role);
            role.IsDeleted = false;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to activate role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return Unit.Value;
        }
    }
}
