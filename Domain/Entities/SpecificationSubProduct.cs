namespace Domain.Entities;

public class SpecificationSubProduct : BaseEntity
{
    public int IdSubProduct { set; get; }
    public int IdSpecification { set; get; }
    public required SubProduct SubProduct { set; get; }
    public required Specification Specification { set; get; }
}
