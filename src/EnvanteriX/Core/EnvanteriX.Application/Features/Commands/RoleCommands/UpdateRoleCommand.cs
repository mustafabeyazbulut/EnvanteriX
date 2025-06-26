using MediatR;
using EnvanteriX.Application.Features.Results.RoleResults;

namespace EnvanteriX.Application.Features.Commands.RoleCommands
{
    public class UpdateRoleCommand : IRequest<UpdateRoleCommandResult>
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
