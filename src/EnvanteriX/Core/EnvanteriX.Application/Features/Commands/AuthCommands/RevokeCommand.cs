using MediatR;

namespace EnvanteriX.Application.Features.Commands.AuthCommands
{
    public class RevokeCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}
