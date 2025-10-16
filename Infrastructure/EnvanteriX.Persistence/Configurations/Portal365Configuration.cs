using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnvanteriX.Persistence.Configurations
{
    public class Portal365Configuration:IEntityTypeConfiguration<Portal365>
    {
        public void Configure(EntityTypeBuilder<Portal365> builder)
        {
            // Tablo ismi (opsiyonel, default: Portal365s)
            //builder.ToTable("Portal365Settings");

            // Primary key
            builder.HasKey(p => p.Id); // TenantId benzersiz ise, yoksa Id ekle

            // Kolon ayarları
            builder.Property(p => p.TenantId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ClientId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ClientSecret)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.SenderEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.IsDeleted)
              .HasDefaultValue(false);  // Default kapalı

            // Opsiyonel: Default değerler veya indeksler eklenebilir
            builder.HasIndex(p => p.SenderEmail).IsUnique();
        }
    }
}
