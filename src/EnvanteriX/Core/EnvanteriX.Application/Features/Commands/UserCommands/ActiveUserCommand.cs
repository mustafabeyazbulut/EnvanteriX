using MediatR;

namespace EnvanteriX.Application.Features.Commands.UserCommands
{
    public class ActiveUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
