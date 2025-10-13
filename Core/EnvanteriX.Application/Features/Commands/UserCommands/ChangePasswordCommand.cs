using MediatR;

namespace EnvanteriX.Application.Features.Commands.UserCommands
{
    public class ChangePasswordCommand:IRequest<Unit>
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
