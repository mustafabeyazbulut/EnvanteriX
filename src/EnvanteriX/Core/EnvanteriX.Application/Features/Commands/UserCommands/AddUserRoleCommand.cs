using MediatR;
namespace EnvanteriX.Application.Features.Commands.UserCommands
{
    public class AddUserRoleCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }
}
