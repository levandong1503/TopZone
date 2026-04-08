using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Role
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<RolePermission>? RolePermissions { get; set; }
    public ICollection<Staff>? Staffs { get; set; }
}
