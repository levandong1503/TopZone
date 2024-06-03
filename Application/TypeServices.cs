using Domain.Abstractions;
using Type = Domain.Entities.Type;

namespace Application;

public class TypeServices
{
    private readonly ITypeRepository _iTypeRepository;
    public TypeServices(ITypeRepository iTypeRepository)
    {
        _iTypeRepository = iTypeRepository;
    }

    public void Add(Type type)
    {
        _iTypeRepository.Add(type);
    }

    public Type GetById(int id)
    {
        return _iTypeRepository.GetById(id);
    }

    //public ICollection<Type> GetTopProductForEveryType(int numberOfType, int numberOfProduct)
    //{
    //    foreach (var item in _iTypeRepository.Gets(numberOfType))
    //    {
    //        item.TypeProducts
    //    }

    //}

}
