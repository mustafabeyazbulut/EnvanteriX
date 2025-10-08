namespace EnvanteriX.WebUI.ViewModels.User
{
    public class CreateUserViewModel
    {
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
