using Application.Interface;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models;
using System.Runtime.InteropServices;

namespace Application;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ITypeRepository _typeRepository;
    private readonly ITypeProductRepository _typeProductRepository;
    public ProductService(IProductRepository productRepository, ITypeRepository typeRepository, ITypeProductRepository typeProductRepository)
    {
        _productRepository = productRepository;
        _typeRepository = typeRepository;
        _typeProductRepository = typeProductRepository;
    }

    public Product Add(Product product)
    {
        return _productRepository.Add(product);
    }

    public Product GetById(int id)
    {
        return _productRepository.GetById(id);
    }

    public IEnumerable<Product> GetHotProductsOfType(int typeId, int productTaking = 5)
    {

        var type = _typeRepository.GetById(typeId) ?? throw new TypeNotFoundException();
        return _productRepository.GetListProductOfType(typeId, productTaking);
    }

    public IEnumerable<Product> GetProductsByNameType(string name)
        => _productRepository.GetProductsByTypeName(name);

    public Product GetDetailProduct(int id)
        => _productRepository.GetDetailProduct(id);
}
