using Domain.Entities;
using Domain.Models;

namespace Domain.Abstractions;

public interface IProductRepository
{
    void Insert(Product product);
    void Update(Product product);
    void Delete(Product product);
    ICollection<Product> GetAll();
    Product GetById(int id);
    ICollection<Product> GetByName(string name);
    ProducsOfType? GetListProductOfType(int idType);
    IEnumerable<Product> GetProductsByTypeName(string name);
    Product GetDetailProduct(int id);
}
