using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Type = Domain.Entities.Type;

namespace Infrastructure.Configurations
{
    internal class TypeConfiguration : IEntityTypeConfiguration<Type>
    {
        public void Configure(EntityTypeBuilder<Type> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.TypeProducts)
                .WithOne(t => t.Type)
                .HasForeignKey(t => t.IdType);

            builder.HasOne(t => t.MainType)
                .WithMany(t => t.SubTypes)
                .HasForeignKey(t => t.MainTypeId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
