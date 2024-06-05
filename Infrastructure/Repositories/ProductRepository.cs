using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Type = Domain.Entities.Type;

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

    public ProducsOfType? GetListProductOfType(int idType)
    {
        var productOfType = _topZoneContext.Types
        .Join(_topZoneContext.TypeProducts, t => t.Id, tp => tp.IdType, (type, sGroup) => new { type, sGroup })
        .GroupJoin(_topZoneContext.Products, t => t.sGroup.IdProduct, p => p.Id, (t, s) => new ProducsOfType() { Type = t.type, Products = s })
        .Where(tp => tp.Type.Id == idType).FirstOrDefault();

        return productOfType;
    }

    public IEnumerable<Product> GetProductsByTypeName(string name)
    {
        var result = _topZoneContext.Types
            .Join(_topZoneContext.TypeProducts, t => t.Id, tp => tp.IdType, (t, tp) => new { t, tp })
            .Join(_topZoneContext.Products, t => t.tp.IdProduct, p => p.Id, (ttp, p ) => new { Type = ttp.t , Product = p })
            .Where( p => p.Type.Name == name)
            .Select( p => p.Product).ToList();

        return result;
    }

    public Product GetDetailProduct(int id)
    {
        var result = _topZoneContext.Products.Find(id);
        
        if(result is null)
        {
            throw new ProductNotFoundException();
        }
        return result;
    }
}

