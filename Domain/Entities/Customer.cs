using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Domain.Entities;

[Index(nameof(UserId), IsUnique = true)]
public class Customer
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; } // FK to ApplicationUser.Id (string)

    [ForeignKey(nameof(UserId))]
    public ApplicationUser? User { get; set; }

    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public int LoyaltyPoints { get; set; } = 0;
}
