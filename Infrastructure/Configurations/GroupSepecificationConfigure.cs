using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class GroupSepecificationConfigure : IEntityTypeConfiguration<GroupSpecification>
    {
        public void Configure(EntityTypeBuilder<GroupSpecification> builder)
        {
            builder.HasKey(x => x.Id);


        }
    }
}
