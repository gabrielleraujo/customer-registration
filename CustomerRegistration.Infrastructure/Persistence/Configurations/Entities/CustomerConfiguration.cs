using CustomerRegistration.Domain.Models.Entities;
using CustomerRegistration.Domain.Models.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerRegistration.Infrastructure.Persistence.Configurations.Entities;

public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.ConfigureBaseEntity();

        builder.OwnsOne(x => x.Name, y =>
            {
                y.Property(y => y.First)
                    .HasColumnName("FirstName")
                    .IsRequired();

                y.Property(y => y.Last)
                    .HasColumnName("LastName")
                    .IsRequired();
            });

        builder.Property(x => x.CellPhone)
            .HasColumnName("CellPhone")
                .HasConversion(
                    x => x.Number,
                    number => new CellPhone(number)
                ).IsRequired(); 

        builder.Property(x => x.MainEmail)
            .HasColumnName("MainEmail")
                .HasConversion(
                    x => x.Text,
                    text => new Email(text)
                ).IsRequired(); 
        
        builder.Property(x => x.RecoveryEmail)
            .HasColumnName("RecoveryEmail")
                .HasConversion(
                    x => x == null ? null : x.Text,
                    text => text == null ? null : new Email(text)
                ).IsRequired(false); 

        builder
            .HasMany(x => x.ClassifiedAdresses)
            .WithOne()
            .HasForeignKey(x => x.CustomerId); 
    }
}
