using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using System.Security.Cryptography.X509Certificates;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly TopZoneContext _topZoneContext;

    public ProductRepository(TopZoneContext topZoneContext)
    {
        _topZoneContext = topZoneContext;
    }

    public void Delete(Product product)
    {
       _topZoneContext.Products.Remove(product);
       _topZoneContext.SaveChanges();
    }

    public ICollection<Product> GetAll()
    {
        return _topZoneContext.Products.ToList();
    }

    public Product GetById(int id)
    {
        var product = _topZoneContext.Products.Find(id);

        if (product == null) 
        {
            throw new ProductNotFoundException();
        }
        return product;
    }

    public ICollection<Product> GetByName(string name)
    {
        var products = _topZoneContext.Products.Where(p => p.ProductName.Contains(name)).ToList();
        return products;
    }

    public void Insert(Product product)
    {
        _topZoneContext.Products.Add(product);
        _topZoneContext.SaveChanges();
    }

    public void Update(Product product)
    {
        _topZoneContext.Products.Update(product);
        _topZoneContext.SaveChanges();
    }
}
