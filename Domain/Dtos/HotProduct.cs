using Domain.Entities;
using Type = Domain.Entities.Type;

namespace Domain.Dtos;


public class HotProduct
{
	public required Type Type { get; set; }
	public IEnumerable<Product>? Products { get; set; }
}
