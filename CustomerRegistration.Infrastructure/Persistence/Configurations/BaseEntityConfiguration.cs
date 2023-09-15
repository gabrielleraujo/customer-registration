using CustomerRegistration.Domain.Models.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerRegistration.Infrastructure.Persistence.Configurations;

public static class BaseEntityConfiguration
{
    public static void ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
    {
        builder.UseTpcMappingStrategy();

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever();

        builder.Property(x => x.CreateAt)
            .HasColumnName("CreateAt")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.LastUpdate)
            .HasColumnName("LastUpdate")
            .HasColumnType("datetime")
            .IsRequired(false);
    }
}
