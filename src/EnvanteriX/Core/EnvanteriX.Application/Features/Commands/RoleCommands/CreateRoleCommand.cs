using MediatR;
using EnvanteriX.Application.Features.Results.RoleResults;

namespace EnvanteriX.Application.Features.Commands.RoleCommands
{
    public class CreateRoleCommand : IRequest<CreateRoleCommandResult>
    {
        public string RoleName { get; set; }
    }
}
