using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TopZone.Dtos;

public class RegisterUserRequest
{
    [Required]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    [AllowNull]
    public string? Address { get; set; }
}
