using MediatR;

namespace EnvanteriX.Application.Features.Commands.UserCommands
{
    public class DeActiveUserCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
