using Domain.Entities;
using Type = Domain.Entities.Type;
namespace Domain.Models;

public class ProducsOfType
{
    public required Type Type { get; set; }
    public IEnumerable<Product>? Products { get; set; }
}
