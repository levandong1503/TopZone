namespace Domain.Entities;

public class SubProduct : BaseEntity
{
    public int IdProduct { get; set; }
    public required Product Product { get; set; }
    public ICollection<SpecificationSubProduct>? SpecificationSubProducts { get; set; }
    public ICollection<ImagesProduct>? ImagesProduct { get; set; }
}
