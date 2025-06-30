using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.Property(x => x.AssetTag)
                   .IsRequired()      // Zorunlu yapar (NOT NULL)
                   .HasMaxLength(50); // Maksimum 50 karakter

            builder.Property(x => x.SerialNumber).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(255);
            builder.Property(x => x.Status).HasMaxLength(50);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}
