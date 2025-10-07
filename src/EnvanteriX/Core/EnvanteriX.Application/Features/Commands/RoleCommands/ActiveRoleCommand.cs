using MediatR;

namespace EnvanteriX.Application.Features.Commands.RoleCommands
{
    public class ActiveRoleCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
