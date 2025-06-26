using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(x => x.Building)
                .HasMaxLength(100);

            builder.Property(x => x.Floor)
                .HasMaxLength(50);

            builder.Property(x => x.Room)
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(255);

            // İstersen Assets koleksiyonu için ilişki kurulabilir ama zorunlu değil
            // builder.HasMany(x => x.Assets)
            //       .WithOne(a => a.Location)
            //       .HasForeignKey(a => a.LocationId);
        }
    }
}
