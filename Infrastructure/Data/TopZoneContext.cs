using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Type = Domain.Entities.Type;

namespace Infrastructure.Data;

public class TopZoneContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<GroupSpecification> GroupSpecifications { get; set; }
    public DbSet<Specification> Specifications { get; set; }
    public DbSet<SpecificationSubProduct> SpecificationSubProducts { get; set; }
    public DbSet<SubProduct> SubProducts { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<TypeProduct> TypeProducts { get; set; }

    public TopZoneContext(DbContextOptions<TopZoneContext> options) : base(options) 
    { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
