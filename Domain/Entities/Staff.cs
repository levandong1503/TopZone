namespace Domain.Entities;

[Index(nameof(UserId), IsUnique = true)]
public class Staff
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; } // FK to ApplicationUser.Id (string)

    [ForeignKey(nameof(UserId))]
    public ApplicationUser? User { get; set; }

    public string? FullName { get; set; }
    public string? EmployeeCode { get; set; }
    public string? Department { get; set; }
    public DateTime? HiredAt { get; set; }
    public bool IsActive { get; set; } = true;
}
