using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SpecificationSubProductConfigure : IEntityTypeConfiguration<SpecificationSubProduct>
{
    public void Configure(EntityTypeBuilder<SpecificationSubProduct> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.SubProduct)
            .WithMany(s => s.SpecificationSubProducts)
            .HasForeignKey(s => s.IdSubProduct);

        builder.HasOne(s => s.Specification)
            .WithMany(s => s.SpecificationSubProducts)
            .HasForeignKey(s => s.IdSpecification);
    }
}
