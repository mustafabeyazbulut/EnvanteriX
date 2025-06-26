using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Configurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.Property(x => x.ModelName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(x => x.Brand)
                .WithMany(b => b.Models)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            // Assets koleksiyonu navigation property, konfigürasyona gerek yoksa bırakabilirsin
        }
    }
}
