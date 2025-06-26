using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EnvanteriX.Application.Interfaces.Repositories;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using EnvanteriX.Persistence.Context;
using EnvanteriX.Persistence.Repositories;
using EnvanteriX.Persistence.UnitOfWorks;


namespace EnvanteriX.Persistence
{
    public static class Registration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EnvanteriXContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // UnitOfWork, bir işlem sırasında birden fazla repository sınıfı üzerinde işlem yapılmasını sağlar.

            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 2;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.SignIn.RequireConfirmedEmail = false;
            })
            .AddRoles<Role>() // Rolleri ekle
            .AddEntityFrameworkStores<EnvanteriXContext>();

            //using (var scope = services.BuildServiceProvider().CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<EnvanteriXContext>();
            //    dbContext.Database.Migrate(); // Migration işlemlerini uygular.
            //}
        }
    }
}
