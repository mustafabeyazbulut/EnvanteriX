using Microsoft.Extensions.DependencyInjection;
using EnvanteriX.Application.Interfaces.AutoMapper;

namespace EnvanteriX.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, AutoMapper.Mapper>();
        }
    }
}
