using CustomerRegistration.Domain.Models.Entities;
using CustomerRegistration.Infrastructure.Persistence.Configurations.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerRegistration.Infrastructure.Persistence;

public class CustomerRegistrationContext: DbContext
{
    public CustomerRegistrationContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ClassifiedAddress> ClassifiedAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new ClassifiedAddressConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}