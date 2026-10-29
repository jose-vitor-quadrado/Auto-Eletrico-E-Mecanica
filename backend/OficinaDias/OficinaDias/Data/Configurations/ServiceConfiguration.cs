using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaDias.Models;

namespace OficinaDias.Data.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Description)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.SuggestedPrice)
            .HasPrecision(18, 2)
            .IsRequired();
    }
}
