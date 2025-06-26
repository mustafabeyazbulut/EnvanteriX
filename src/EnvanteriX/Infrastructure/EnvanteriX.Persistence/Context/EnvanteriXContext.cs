using EnvanteriX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EnvanteriX.Persistence.Context
{
    public class EnvanteriXContext : IdentityDbContext<User, Role, int>
    {
        public EnvanteriXContext() { }

        public EnvanteriXContext(DbContextOptions<EnvanteriXContext> options)
            : base(options) { }

        // DbSet'ler public olmalı, böylece dışarıdan erişilebilir.
       
       

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<SoftwareLicense> SoftwareLicenses { get; set; }
        public DbSet<AssetMovement> AssetMovements { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API ile yapılacak diğer özelleştirmeler
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Entity konfigürasyonlarını uygula
        }
    }

}
