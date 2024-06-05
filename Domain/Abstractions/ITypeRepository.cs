namespace Domain.Abstractions;

using Domain.Models;
using Type = Entities.Type;
public interface ITypeRepository
{
    void Add(Type type);
    void Update(Type type);
    void Delete(Type type);
    ICollection<Type> GetAll();
    Type GetById(int id);
    ICollection<Type> GetByName(string name);
    public ICollection<Type> Gets(int number);
    public IEnumerable<Type> GetNumberOfTypes(int numberOfTypes);

}
