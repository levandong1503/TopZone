using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SpecificationConfigure : IEntityTypeConfiguration<Specification>
{
    public void Configure(EntityTypeBuilder<Specification> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(s => s.GroupSpecification)
            .WithMany(g => g.Specifications)
            .HasForeignKey(g => g.GroupId);
    }
}
