using bruno.BankSystem.Domain.Interfaces.Persistence;
using bruno.BankSystem.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bruno.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {      
            services.AddScoped<IAccountRepository, FakeAccountRepository>();
            services.AddScoped<IUserRepository, FakeUserRepository>();

            return services;    
        }

    }
}
