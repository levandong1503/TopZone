using System.ComponentModel.DataAnnotations;

namespace TopZone.Dtos;

public class RefreshRequest
{
    [Required]
    public string RefreshToken { get; set; } = null!;

    public string? DeviceInfo { get; set; }
}