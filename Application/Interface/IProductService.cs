using Domain.Dtos;
using Domain.Entities;

namespace Application.Interface;

public interface IProductService
{
    public Task<Product> Add(ProductRequest product);
    public Product GetById(int id);
    public IEnumerable<Product> GetHotProductsOfType(int typeId, int productTaking = 5);
    public IEnumerable<Product> GetProductsByNameType(string name);
    public Product GetDetailProduct(int id);
}
