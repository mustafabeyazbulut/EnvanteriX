using MediatR;

namespace EnvanteriX.Application.Features.Commands.RoleCommands
{
    public class DeActiveRoleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
