namespace Domain.Entities;

public class GroupSpecification : BaseEntity
{
    public string? Name { get; set; }
    public int GroupId { get; set; } 
    public ICollection<Specification> Specifications { get; set; }
}
