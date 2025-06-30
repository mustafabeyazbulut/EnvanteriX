using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.BrandName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        }
    }
}
