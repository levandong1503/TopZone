namespace TopZone.Dtos;

public class ProductDto
{
    public required string ProductName { get; set; }
    public string? Description { get; set; }
    public required ICollection<int> types {  get; set; } 
}
