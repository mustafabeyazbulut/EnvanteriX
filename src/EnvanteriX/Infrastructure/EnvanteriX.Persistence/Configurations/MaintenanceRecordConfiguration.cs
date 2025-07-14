using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Configurations
{
    public class MaintenanceRecordConfiguration : IEntityTypeConfiguration<MaintenanceRecord>
    {
        public void Configure(EntityTypeBuilder<MaintenanceRecord> builder)
        {
            builder.HasIndex(x => new { x.AssetId, x.StartDate })
                     .IsUnique();

            builder.Property(x => x.PerformedBy)
                .HasMaxLength(100);

            builder.Property(x => x.PreServiceDescription)
                .HasMaxLength(255);

            builder.Property(x => x.PostServiceDescription)
               .HasMaxLength(255);

            builder.Property(x => x.Cost)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.Property(x => x.EndDate);

            // İlişkiler (opsiyonel ama tavsiye edilir)
            builder.HasOne(x => x.Asset)
                .WithMany(a => a.MaintenanceRecords)
                .HasForeignKey(x => x.AssetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Vendor)
                .WithMany(v => v.MaintenanceRecords)
                .HasForeignKey(x => x.VendorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        }
    }
}
