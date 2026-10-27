using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaDias.Models;

namespace OficinaDias.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer> 
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Phone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.Neighborhood)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Street)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasMany(c => c.Vehicles)
            .WithOne(v => v.Customer)
            .HasForeignKey(v => v.CustomerId);

        builder.HasMany(c => c.ServiceOrders)
            .WithOne(so => so.Customer)
            .HasForeignKey(so => so.CustomerId);
    }
}
