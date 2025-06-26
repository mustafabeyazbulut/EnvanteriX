using MediatR;

namespace EnvanteriX.Application.Features.Commands.RoleCommands
{
    public class DeleteRoleCommand : IRequest<Unit>
    {
        public int RoleId { get; set; }
        public DeleteRoleCommand(int id) => RoleId = id;
    }
}
