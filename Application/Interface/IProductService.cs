using Domain.Entities;
using Domain.Models;

namespace Application.Interface;

public interface IProductService
{
    public void Add(Product product);
    public Product GetById(int id);
    public IEnumerable<ProducsOfType> GetHotProductsOfTypeList(int numberOfType);
    public IEnumerable<Product> GetProductsByNameType(string name);
    public Product GetDetailProduct(int id);
}
