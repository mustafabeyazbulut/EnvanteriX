using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Domain.Entities
{
    // +
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
