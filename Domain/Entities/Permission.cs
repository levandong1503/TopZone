using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[Index(nameof(Code), IsUnique = true)]
public class Permission
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Code { get; set; } = null!; // UK

    public string? Name { get; set; }
    public string? Module { get; set; }
    public string? Action { get; set; }

    public ICollection<RolePermission>? RolePermissions { get; set; }
}
