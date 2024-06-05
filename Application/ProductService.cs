using Application.Interface;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Models;
using System.Runtime.InteropServices;

namespace Application;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ITypeRepository _typeRepository;
    public ProductService(IProductRepository productRepository, ITypeRepository typeRepository)
    {
        _productRepository = productRepository;
        _typeRepository = typeRepository;
    }

    public void Add(Product product)
    {
        _productRepository.Insert(product);
    }

    public Product GetById(int id)
    {
        return _productRepository.GetById(id);
    }

    public IEnumerable<ProducsOfType> GetHotProductsOfTypeList(int numberOfType)
    {

        var types = _typeRepository.GetNumberOfTypes(numberOfType); //_productRepository.GetListProductOfType(numberOfType);
        var productsOfTypes = new List<ProducsOfType>();

        foreach (var item in types)
        {
            var itemHomeGroup = _productRepository.GetListProductOfType(item.Id);
            
            if (itemHomeGroup != null)
            {
                productsOfTypes.Add(itemHomeGroup);
            }
        }

        return productsOfTypes;
    }

    public IEnumerable<Product> GetProductsByNameType(string name)
        => _productRepository.GetProductsByTypeName(name);

    public Product GetDetailProduct(int id)
        => _productRepository.GetDetailProduct(id);
}
