using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Index(nameof(Token), IsUnique = true)]
public class RefreshToken
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Token { get; set; } = null!;

    [Required]
    public Guid ApplicationUserId { get; set; } // FK to ApplicationUser.Id (string)

    [ForeignKey(nameof(ApplicationUserId))]
    public ApplicationUser? ApplicationUser { get; set; }

    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public string? DeviceInfo { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
