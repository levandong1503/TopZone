using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TypeProductConfigure : IEntityTypeConfiguration<TypeProduct>
{
    public void Configure(EntityTypeBuilder<TypeProduct> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Product)
            .WithMany(sp => sp.TypeProducts)
            .HasForeignKey(sp => sp.IdProduct);

        builder.HasOne(t => t.Type)
            .WithMany(t => t.TypeProducts)
            .HasForeignKey(t => t.IdType);
    }
}
