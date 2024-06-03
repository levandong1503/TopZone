using Domain.Abstractions;
using Domain.Entities;
using System.Runtime.InteropServices;

namespace Application;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void Add(Product product)
    {
        _productRepository.Insert(product);
    }

    public Product GetById(int id)
    {
        return _productRepository.GetById(id);
    }
}
