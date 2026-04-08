using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Type = Domain.Entities.Type;

namespace Infrastructure.Data;

public class TopZoneContext : DbContext
{
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<GroupSpecification> GroupSpecifications { get; set; }
    public DbSet<Specification> Specifications { get; set; }
    public DbSet<SpecificationSubProduct> SpecificationSubProducts { get; set; }
    public DbSet<SubProduct> SubProducts { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<TypeProduct> TypeProducts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    public TopZoneContext(DbContextOptions<TopZoneContext> options) : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure RolePermission composite PK
        builder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        // One-to-one between ApplicationUser and Customer
        builder.Entity<Customer>()
            .HasOne(c => c.User)
            .WithOne(u => u.Customer)
            .HasForeignKey<Customer>(c => c.UserId);

        // One-to-one between ApplicationUser and Staff
        builder.Entity<Staff>()
            .HasOne(s => s.User)
            .WithOne(u => u.Staff)
            .HasForeignKey<Staff>(s => s.UserId);

        // Role - RolePermission
        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        // Permission - RolePermission
        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId);
    }

    /// <summary>
    /// you can use to customize convention
    /// </summary>
    /// <param name="configurationBuilder"></param>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }
}
