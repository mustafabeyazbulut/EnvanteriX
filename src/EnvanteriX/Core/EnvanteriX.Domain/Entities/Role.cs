using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
