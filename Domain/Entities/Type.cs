namespace Domain.Entities;

public class Type : BaseEntity
{
    public required string Name { get; set; }
    public int? MainTypeId { get; set; }
    public Type? MainType { get; set; }
    public ICollection<TypeProduct>? TypeProducts { get; set; }
    public ICollection<Type>? SubTypes { get; set; }
}
