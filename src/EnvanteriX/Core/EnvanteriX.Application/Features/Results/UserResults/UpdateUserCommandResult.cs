
namespace EnvanteriX.Application.Features.Results.UserResults
{
    public class UpdateUserCommandResult
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public UpdateUserCommandResult() { }

        public UpdateUserCommandResult(Domain.Entities.User user)
        {
            UserId = user.Id;
            FullName = user.FullName;
            UserName = user.UserName;
            Email = user.Email;
            IsActive = user.IsActive;
        }
    }
}
