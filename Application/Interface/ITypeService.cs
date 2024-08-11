namespace Application.Interface;
using Type = Domain.Entities.Type;

public interface ITypeService
{
    Task Add(Type type);
    Type GetById(int id);
}
