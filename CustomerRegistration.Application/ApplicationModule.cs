using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using CustomerRegistration.Application.Validations;

namespace CustomerRegistration.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddValidators()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddClassifiedAddressCommandValidation>(ServiceLifetime.Scoped);
        services.AddValidatorsFromAssemblyContaining<AddRecoveryEmailCommandValidation>(ServiceLifetime.Scoped);
        services.AddValidatorsFromAssemblyContaining<RegisterCustomerCommandValidator>(ServiceLifetime.Scoped);

        return services;
    }
}
