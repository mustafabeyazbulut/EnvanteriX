using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Configurations
{
    public class AssetTypeConfiguration : IEntityTypeConfiguration<AssetType>
    {
        public void Configure(EntityTypeBuilder<AssetType> builder)
        {
            builder.Property(x => x.TypeName)
                .IsRequired()           // Required olduğu için bunu belirtmek iyi olur
                .HasMaxLength(100);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            // AssetType birçok varlığa sahip olabilir, bu nedenle varlıkların AssetType ile ilişkisini tanımlıyoruz.
            builder.HasMany(x => x.Assets)
                   .WithOne(a => a.AssetType)
                   .HasForeignKey(a => a.AssetTypeId);

        }
    }
}
