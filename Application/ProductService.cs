using Application.Interface;
using AutoMapper;
using Domain.Abstractions;
using Domain.Dtos;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;

namespace Application;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Product> Add(ProductRequest productRequest)
    {
		await _unitOfWork.BeginTransactionAsync();

		var alltypeId = _unitOfWork.TypeRepository.GetAll().Select(t => t.Id);

        if(!productRequest.types.All(typeId => alltypeId.Contains(typeId)))
        {
            throw new TypeNotFoundException();
        }

		var addProduct = _mapper.Map<Product>(productRequest);
        var mewProduct = _unitOfWork.ProductRepository.Add(addProduct);
        await _unitOfWork.CommitAsync();

        var typeProducts = new List<TypeProduct>();

		foreach (var typeId in productRequest.types)
		{
            typeProducts.Add(new TypeProduct() 
            { 
                Product = addProduct,
                Type = _unitOfWork.TypeRepository.GetById(typeId) 
            });
		}

        await _unitOfWork.BeginTransactionAsync();
		_unitOfWork.TypeProductRepository.AddRange(typeProducts);
        await _unitOfWork.CommitAsync();

		return mewProduct;
    }

    public Product GetById(int id)
    {
        return _unitOfWork.ProductRepository.GetById(id);
    }

    public IEnumerable<Product> GetHotProductsOfType(int typeId, int productTaking = 5)
    {

        var type = _unitOfWork.TypeRepository.GetById(typeId) ?? throw new TypeNotFoundException();
        return _unitOfWork.ProductRepository.GetListProductOfType(typeId, productTaking);
    }

    public IEnumerable<Product> GetProductsByNameType(string name)
        => _unitOfWork.ProductRepository.GetProductsByTypeName(name);

    public Product GetDetailProduct(int id)
        => _unitOfWork.ProductRepository.GetDetailProduct(id);
}
