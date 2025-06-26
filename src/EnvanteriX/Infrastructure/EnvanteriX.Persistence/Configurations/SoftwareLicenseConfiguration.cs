using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Configurations
{
    public class SoftwareLicenseConfiguration : IEntityTypeConfiguration<SoftwareLicense>
    {
        public void Configure(EntityTypeBuilder<SoftwareLicense> builder)
        {
            builder.Property(x => x.LicenseKey)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.SoftwareName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Version)
                .HasMaxLength(50);

            builder.Property(x => x.PurchaseDate)
                .IsRequired();

            builder.Property(x => x.ExpiryDate)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            // İlişkiler
            builder.HasOne(x => x.Asset)
                .WithMany(a => a.SoftwareLicenses)
                .HasForeignKey(x => x.AssetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Vendor)
              .WithMany(a => a.Licenses)
              .HasForeignKey(x => x.VendorId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssignedUser)
                .WithMany()
                .HasForeignKey(x => x.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
