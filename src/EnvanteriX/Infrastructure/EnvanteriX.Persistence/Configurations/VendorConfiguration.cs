using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Configurations
{
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.Property(x => x.VendorName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ContactPerson)
                .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasMaxLength(100);

            // İlişkiler (Opsiyonel, eğer Fluent API ile konfigüre etmek isterseniz)
            builder.HasMany(v => v.Assets)
                .WithOne(a => a.Vendor)
                .HasForeignKey(a => a.VendorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(v => v.Licenses)
                .WithOne(l => l.Vendor)
                .HasForeignKey(l => l.VendorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(v => v.MaintenanceRecords)
                .WithOne(m => m.Vendor)
                .HasForeignKey(m => m.VendorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        }
    }
}
