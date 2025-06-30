using MediatR;

namespace EnvanteriX.Application.Features.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public UpdateUserCommand(int userId, string fullName, string userName, string email)
        {
            UserId = userId;
            FullName = fullName;
            UserName = userName;
            Email = email;
        }
    }
}
