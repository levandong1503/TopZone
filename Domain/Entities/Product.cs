namespace Domain.Entities;

public class Product : BaseEntity
{
    public required string ProductName { set; get; }
    public string? Description { set; get; }
    public ICollection<SubProduct>? SubProducts { set; get; }
    public ICollection<TypeProduct>? TypeProducts { set; get; }
}
