using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Configurations
{
    public class AssetMovementConfiguration : IEntityTypeConfiguration<AssetMovement>
    {
        public void Configure(EntityTypeBuilder<AssetMovement> builder)
        {
            // Birincil anahtar (base class EntityBase'den geliyorsa zaten vardır,
            // eğer yoksa eklemelisin, örneğin Id)
            builder.HasKey(x => x.Id);

            // İlişkiler (opsiyonel, ama tavsiye edilir)
            builder.HasOne(x => x.Asset)
                .WithMany()   // Eğer Asset tarafında ICollection<AssetMovement> varsa onu yazabilirsin
                .HasForeignKey(x => x.AssetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.FromUser)
                .WithMany()
                .HasForeignKey(x => x.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ToUser)
                .WithMany()
                .HasForeignKey(x => x.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.FromLocation)
                .WithMany()
                .HasForeignKey(x => x.FromLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ToLocation)
                .WithMany()
                .HasForeignKey(x => x.ToLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Note)
                .HasMaxLength(255);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        }
    }
}
