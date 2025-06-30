
namespace EnvanteriX.Application.Features.Results.UserResults
{
    public class GetAllUsersQueryResult
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
