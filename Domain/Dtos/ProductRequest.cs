namespace Domain.Dtos;
public class ProductRequest
{
	public required string ProductName { get; set; }
	public string? Description { get; set; }
	public required ICollection<int> types { get; set; }
}
