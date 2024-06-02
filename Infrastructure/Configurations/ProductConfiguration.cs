using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(nameof(Product.Id));
        builder.HasOne(p => p.Type);

        builder.Property(p => p.ProductName)
            .HasColumnType("nvarchar")
            .HasMaxLength(255);

        builder.Property(p => p.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(6500);

    }
}
