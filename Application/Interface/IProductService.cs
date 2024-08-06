using Domain.Entities;
using Domain.Models;

namespace Application.Interface;

public interface IProductService
{
    public Product Add(Product product);
    public Product GetById(int id);
    public IEnumerable<Product> GetHotProductsOfType(int typeId, int productTaking = 5);
    public IEnumerable<Product> GetProductsByNameType(string name);
    public Product GetDetailProduct(int id);
}
