namespace Application.Interface;
using Type = Domain.Entities.Type;

public interface ITypeService
{
    void Add(Type type);
    Type GetById(int id);
}
