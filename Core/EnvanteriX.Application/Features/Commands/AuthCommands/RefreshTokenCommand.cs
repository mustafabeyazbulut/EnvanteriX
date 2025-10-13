using EnvanteriX.Application.Features.Results.AuthResults;
using MediatR;

namespace EnvanteriX.Application.Features.Commands.AuthCommands
{
    public class RefreshTokenCommand : IRequest<RefreshTokenCommandResult>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
