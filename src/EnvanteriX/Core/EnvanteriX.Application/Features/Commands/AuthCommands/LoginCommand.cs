using EnvanteriX.Application.Features.Results.AuthResults;
using MediatR;
using System.ComponentModel;


namespace EnvanteriX.Application.Features.Commands.AuthCommands
{
    public class LoginCommand : IRequest<LoginCommandResult>
    {
        [DefaultValue("mbeyazbulut@aundeteknik.com")]
        public string Email { get; set; }
        [DefaultValue("789+Asdf")]
        public string Password { get; set; }
    }
}
