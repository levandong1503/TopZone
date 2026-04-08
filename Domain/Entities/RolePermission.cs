using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace Domain.Entities;

public class RolePermission
{
    // Composite PK (RoleId, PermissionId) — configure in DbContext.OnModelCreating
    public Guid RoleId { get; set; }
    [ForeignKey(nameof(RoleId))]
    public Role? Role { get; set; }

    public Guid PermissionId { get; set; }
    [ForeignKey(nameof(PermissionId))]
    public Permission? Permission { get; set; }
}
