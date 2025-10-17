using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EnvanteriX.Domain.Enums;

namespace EnvanteriX.Persistence.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.Property(x => x.AssetTag)
             .IsRequired()
             .HasMaxLength(50);

            builder.Property(x => x.SerialNumber)
                   .HasMaxLength(100);

            builder.Property(x => x.Description)
            .HasMaxLength(255);

            builder.Property(a => a.Status)
       .HasConversion<string>()
       .HasDefaultValue(StatusEnum.Stokta)
       .HasSentinel(StatusEnum.None);


            builder.Property(x => x.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(x => x.IsRented)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(x => x.RentalStartDate)
                   .HasColumnType("datetime")
                   .IsRequired(false);

            builder.Property(x => x.Description)
               .IsRequired(false)
               .HasMaxLength(255);


            // bu alttaki kod
            // AssetType, Model, Vendor, Location ve AssignedUser ile ilişkileri tanımlıyoruz

            builder.HasOne(x => x.AssetType)
                   .WithMany()
                   .HasForeignKey(x => x.AssetTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Model)
                   .WithMany()
                   .HasForeignKey(x => x.ModelId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Vendor)
                   .WithMany()
                   .HasForeignKey(x => x.VendorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Location)
                   .WithMany()
                   .HasForeignKey(x => x.LocationId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssignedUser)
                   .WithMany()
                   .HasForeignKey(x => x.AssignedUserId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.AssignedDepartment)
             .WithMany()
             .HasForeignKey(x => x.AssignedDepartmentId)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.SetNull);

            builder.Property(x => x.CreatedDate)
                    .HasDefaultValueSql("GETDATE()"); // SQL Server için

        }
    }
}
