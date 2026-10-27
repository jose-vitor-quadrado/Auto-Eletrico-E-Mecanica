using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaDias.Models;

namespace OficinaDias.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);

        builder.HasIndex(v => v.LicensePlate)
            .IsUnique();

        builder.Property(v => v.LicensePlate)
            .HasMaxLength(8)
            .IsRequired();

        builder.Property(v => v.Brand)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(v => v.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Color)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(v => v.Mileage)
            .IsRequired();

        builder.HasOne(v => v.Customer)
            .WithMany(c => c.Vehicles)
            .HasForeignKey(v => v.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
