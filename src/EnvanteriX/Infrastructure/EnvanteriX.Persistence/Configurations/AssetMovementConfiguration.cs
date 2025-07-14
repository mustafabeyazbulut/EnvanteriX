using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Configurations
{
    public class AssetMovementConfiguration : IEntityTypeConfiguration<AssetMovement>
    {
        public void Configure(EntityTypeBuilder<AssetMovement> builder)
        {
            builder.HasKey(x => x.Id);

            // İlişkiler (opsiyonel, ama tavsiye edilir)
            builder.HasOne(x => x.Asset) // AssetMovement bir Asset'e ait
                     .WithMany(x => x.AssetMovements) // Asset birçok AssetMovement'a sahip olabilir
                     .HasForeignKey(x => x.AssetId) // AssetMovement tablosunda AssetId foreign key olarak kullanılacak
                     .OnDelete(DeleteBehavior.Restrict); // Asset silinince AssetMovement'lar silinmesin


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
