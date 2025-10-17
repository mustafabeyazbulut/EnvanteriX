using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnvanteriX.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(255);

            builder.Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(255);

            // Location birçok varlığa sahip olabilir, bu nedenle varlıkların Location ile ilişkisini tanımlıyoruz.
            builder.HasMany(x => x.Assets)
                  .WithOne(a => a.AssignedDepartment)
                  .HasForeignKey(a => a.AssignedDepartmentId);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.Property(x => x.CreatedDate)
                   .HasDefaultValueSql("GETDATE()"); // SQL Server için

        }
    }
}
