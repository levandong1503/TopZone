using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Type = Domain.Entities.Type;

namespace Infrastructure.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    private readonly TopZoneContext _topZoneContext;

    public ProductRepository(TopZoneContext topZoneContext) : base(topZoneContext)
    {
        _topZoneContext = topZoneContext;
    }

    public ICollection<Product> GetAll()
    {
        return _topZoneContext.Products.ToList();
    }

    public override Product GetById(int id)
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

    public IEnumerable<Product> GetListProductOfType(int idType, int take = 5)
    {
        //var productOfType = _topZoneContext.Types
        //.Join(_topZoneContext.TypeProducts, t => t.Id, tp => tp.IdType, (type, sGroup) => new { type, sGroup })
        //.GroupJoin(_topZoneContext.Products, t => t.sGroup.IdProduct, p => p.Id, (t, s) => new { t.type, s })
        //.Where(tp => tp.type.Id == idType)
        //.SelectMany(x => x.s)
        //.Take(take)
        //.ToList();

        var productOfType = _topZoneContext.Products
            .Include(nameof(Product.TypeProducts))
            .Where(p => p.TypeProducts.Any(tp => tp.Id == idType))
            .ToList();

        return productOfType;
    }

    public IEnumerable<ProducsOfType> GetHotProduct(int numberOfType = 1, int numberOfProduct = 5)
    {
        return _topZoneContext.TypeProducts.GroupJoin(_topZoneContext.Products,
            tp => tp.IdProduct, 
            p => p.Id,
            (typeProduct, products) => new { typeProduct, products })
            .Join(_topZoneContext.Types, tp => tp.typeProduct.IdType, 
                t => t.Id, 
                (tp, type) 
                    => new ProducsOfType() { Type = type, Products = tp.products.Take(numberOfType) })
                        .Take(numberOfType);
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

