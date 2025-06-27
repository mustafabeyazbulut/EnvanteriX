using MediatR;

namespace EnvanteriX.Application.Features.Commands.RoleCommands
{
    public class UpdateRoleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
