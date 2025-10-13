using MediatR;

namespace EnvanteriX.Application.Features.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public int UserId { get; set; }

        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }
    }
}
