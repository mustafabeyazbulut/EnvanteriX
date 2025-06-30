using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnvanteriX.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FullName).HasMaxLength(250);
            builder.Property(x => x.UserName).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.PasswordHash).HasMaxLength(100);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        }
    }
}
