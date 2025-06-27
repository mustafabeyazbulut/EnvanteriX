using MediatR;

namespace EnvanteriX.Application.Features.Commands.RoleCommands
{
    public class DeleteRoleCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteRoleCommand(int Id)
        {
            this.Id = Id;
        }
    }
}
