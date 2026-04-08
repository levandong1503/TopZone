using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SubProductConfigure : IEntityTypeConfiguration<SubProduct>
{
    public void Configure(EntityTypeBuilder<SubProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(s => s.Product)
            .WithMany(p => p.SubProducts)
            .HasForeignKey(s => s.IdProduct);
    }
}
