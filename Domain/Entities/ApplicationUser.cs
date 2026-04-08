using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ApplicationUser
{
    [Key]
    public Guid UserId { get; set; }
    // Additional fields from ER diagram
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? UserType { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Customer? Customer { get; set; }
    public Staff? Staff { get; set; }
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
