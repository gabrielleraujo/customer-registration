using CustomerRegistration.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomerRegistration.API.Configurations;

public static class EFMigrateConfiguration
{
    public static void UseMigration(this WebApplication? app, IConfiguration configuration)
    {
        // Aplicar migrations ao iniciar a aplicação
        using (var scope = app!.Services.CreateScope())
        {
            if (configuration!.GetValue<bool>("EF_MIGRATION"))
            {
                Console.WriteLine("Migrations...");
                var context = scope.ServiceProvider.GetRequiredService<CustomerRegistrationContext>(); // Substitua pelo seu DbContext
                
                if (context.Database.GetPendingMigrations().Any())
                {
                    Console.WriteLine("Apply Migrations...");
                    context.Database.Migrate(); // Isso aplicará as migrations assim que aplicação for iniciada pela primeira vez.
                    Console.WriteLine("Migrations ok...");
                }
            }
        }
    }
}