using Application.Interface;
using Domain.Abstractions;
using Type = Domain.Entities.Type;

namespace Application;

public class TypeServices : ITypeService
{
    private readonly ITypeRepository _iTypeRepository;

    private readonly IUnitOfWork _unitOfWork;

    public TypeServices(ITypeRepository iTypeRepository, IUnitOfWork unitOfWork)
    {
        _iTypeRepository = iTypeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(Type type)
    {
        //_iTypeRepository.Add(type);
        _unitOfWork.TypeRepository.Add(type);
        await _unitOfWork.SaveChangesAsync(); 
    }

    public Type GetById(int id)
    {
        return _iTypeRepository.GetById(id);
    }
}
