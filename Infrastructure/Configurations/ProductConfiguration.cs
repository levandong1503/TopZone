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

        builder.Property(p => p.ProductName)
            .HasColumnType("nvarchar")
            .HasMaxLength(255);

        builder.Property(p => p.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(2000);

        builder.HasMany(p => p.TypeProducts)
            .WithOne(tp => tp.Product);

    }
}
