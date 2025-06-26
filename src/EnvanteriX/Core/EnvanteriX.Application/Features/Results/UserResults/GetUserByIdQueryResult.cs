
namespace EnvanteriX.Application.Features.Results.UserResults
{
    public class GetUserByIdQueryResult
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
