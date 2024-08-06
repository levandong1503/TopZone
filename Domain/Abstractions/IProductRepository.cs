using Domain.Entities;
using Domain.Models;

namespace Domain.Abstractions;

public interface IProductRepository : IRepositoryBase<Product>
{
    ICollection<Product> GetAll();
    ICollection<Product> GetByName(string name);
    IEnumerable<Product> GetListProductOfType(int idType, int take = 5);
    IEnumerable<Product> GetProductsByTypeName(string name);
    Product GetDetailProduct(int id);
}
