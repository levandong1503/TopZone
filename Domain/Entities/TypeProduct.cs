namespace Domain.Entities;

public class TypeProduct : BaseEntity
{
    public int IdProduct { get; set; }
    public int IdType { get; set; }
    public required Type Type { get; set; }
    public required Product Product { get; set; }
}
