using CustomerRegistration.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerRegistration.Infrastructure.Persistence.Configurations.Entities;

public class ClassifiedAddressConfiguration : IEntityTypeConfiguration<ClassifiedAddress>
{
    public void Configure(EntityTypeBuilder<ClassifiedAddress> builder)
    {
        builder.ToTable("ClassifiedAdresses");

        builder.ConfigureBaseEntity();
            
        builder.Property(x => x.CustomerId)
            .HasColumnName("CustomerId");
        
        builder.Property(x => x.IsMain)
            .HasColumnName("IsMain");
        
        builder.Property(x => x.Classified)
            .HasColumnName("Classified");

        builder.OwnsOne(x => x.Address, y =>
        {
            y.Property(x => x.PostalCode)
            .HasColumnName("PostalCode");
        
            y.Property(x => x.State)
                .HasColumnName("State");

            y.Property(x => x.City)
                .HasColumnName("City");

            y.Property(x => x.Neighborhood)
                .HasColumnName("Neighborhood");

            y.Property(x => x.Street)
                .HasColumnName("Street");

            y.Property(x => x.Number)
                .HasColumnName("Number");

            y.Property(x => x.Complement)
                .HasColumnName("Complement");
        });

        builder.HasIndex(x => x.CustomerId);
    }
}
