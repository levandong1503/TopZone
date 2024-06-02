using System.Security.Cryptography.X509Certificates;

namespace Domain.Entities;

public class Specification : BaseEntity
{
    public required string Name { get; set; }
    public int GroupId { get; set; }
    public required GroupSpecification GroupSpecification { get; set; }
    public ICollection<SpecificationSubProduct>? SpecificationSubProducts { get; set;}
}
