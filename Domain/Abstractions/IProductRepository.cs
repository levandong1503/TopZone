using Domain.Entities;

namespace Domain.Abstractions;

public interface IProductRepository
{
    void Insert(Product product);
    void Update(Product product);
    void Delete(Product product);
    ICollection<Product> GetAll();
    Product GetById(int id);
    ICollection<Product> GetByName(string name);
}
