using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaDias.Models;

namespace OficinaDias.Data.Configurations;

public class ServiceOrderConfiguration : IEntityTypeConfiguration<ServiceOrder>
{
    public void Configure(EntityTypeBuilder<ServiceOrder> builder)
    {
        builder.HasKey(so => so.Id);

        builder.Property(so => so.Description)
            .HasMaxLength(500);

        builder.Property(so => so.OpenDate)
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(so => so.CloseDate);

        builder.Property(so => so.WasPaid)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Ignore(so => so.Total);

        builder.HasOne(so => so.Customer)
            .WithMany(c => c.ServiceOrders)
            .HasForeignKey(so => so.CustomerId);

        builder.HasOne(so => so.Vehicle)
            .WithMany(v => v.ServiceOrders)
            .HasForeignKey(so => so.VehicleId);

        builder.HasMany(so => so.Items)
            .WithOne(i => i.ServiceOrder)
            .HasForeignKey(i => i.ServiceOrderId);
    }
}
