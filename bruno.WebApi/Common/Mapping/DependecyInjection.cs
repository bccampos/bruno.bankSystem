using Mapster;
using MapsterMapper;
using System.Reflection;

namespace bruno.BankSystem.WebApi.Common.Mapping
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();

            return services;
        }
    }
}
