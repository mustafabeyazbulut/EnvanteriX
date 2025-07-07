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

            // bu alttaki kod
            // Model ile Brand arasındaki ilişkiyi yapılandırır.
            // Model, BrandId ile Brand'a bağlıdır.
            // Brand ile Model arasındaki ilişki, bir markanın birden fazla modele sahip olabileceğini belirtir.
            // OnDelete(DeleteBehavior.Cascade) ifadesi, bir marka silindiğinde ilişkili tüm modellerin de silineceğini belirtir.
            builder.HasOne(x => x.Brand)
                .WithMany(b => b.Models)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Assets)
                   .WithOne(a => a.Model)
                   .HasForeignKey(a => a.ModelId);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        }
    }
}
