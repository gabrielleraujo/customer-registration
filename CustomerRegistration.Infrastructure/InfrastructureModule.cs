using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CustomerRegistration.Domain.Repositories;
using CustomerRegistration.Infrastructure.Persistence;
using CustomerRegistration.Infrastructure.Repositories;

namespace CustomerRegistration.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {            
        services
            .AddSqlServer()
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddSqlServer(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        
        services.AddDbContext<CustomerRegistrationContext>(opt => {
            opt.UseSqlServer(configuration!.GetConnectionString("Default"));
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
