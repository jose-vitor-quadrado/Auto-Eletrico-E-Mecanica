using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaDias.Models;

namespace OficinaDias.Data.Configurations;

public class ServiceOrderItemConfiguration : IEntityTypeConfiguration<ServiceOrderItem>
{
    public void Configure(EntityTypeBuilder<ServiceOrderItem> builder)
    {
        builder.HasKey(soi => soi.Id);

        builder.Property(soi => soi.ItemDescription)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(soi => soi.Quantity)
            .IsRequired();

        builder.Property(soi => soi.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasOne(soi => soi.ServiceOrder)
            .WithMany(so => so.Items)
            .HasForeignKey(soi => soi.ServiceOrderId);

        builder.HasOne(soi => soi.Product)
            .WithMany()
            .HasForeignKey(soi => soi.ProductId);

        builder.HasOne(soi => soi.Service)
            .WithMany()
            .HasForeignKey(soi => soi.ServiceId);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_ServiceOrderItem_ProductOrService",
                @"(
                    (ProductId IS NOT NULL AND ServiceId IS NULL)
                    OR
                    (ProductId IS NULL AND ServiceId IS NOT NULL)
                )");
        });
    }
}
