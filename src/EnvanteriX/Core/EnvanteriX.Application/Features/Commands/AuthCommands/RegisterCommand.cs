using MediatR;

namespace EnvanteriX.Application.Features.Commands.AuthCommands
{
    public class RegisterCommand : IRequest<Unit>
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
