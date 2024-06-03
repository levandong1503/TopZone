using Domain.Abstractions;
using Domain.Exceptions;
using Infrastructure.Data;
using Type = Domain.Entities.Type;

namespace Infrastructure.Repositories;

public class TypeRepository : ITypeRepository
{
    private readonly TopZoneContext _topZoneContext;
    public TypeRepository(TopZoneContext topZoneContext)
    {
        _topZoneContext = topZoneContext;
    }

    public void Add(Type type)
    {
        _topZoneContext.Add(type);
        _topZoneContext.SaveChanges();
    }

    public void Delete(Type type)
    {
        _topZoneContext.Remove(type);
        _topZoneContext.SaveChanges();
    }

    public ICollection<Type> GetAll()
    {
        return _topZoneContext.Types.ToList();
    }

    public Type GetById(int id)
    {
        var type = _topZoneContext.Types.Find(id);
        
        if (type == null)
        {
            throw new TypeNotFoundException();
        }

        return type;
    }

    public ICollection<Type> GetByName(string name)
    {
        return _topZoneContext.Types.Where(t => t.Name.Contains(name)).ToList();
    }

    public void Update(Type type)
    {
        _topZoneContext.Update(type);
        _topZoneContext.SaveChanges();
    }

    public ICollection<Type> Gets(int number)
    {
        return _topZoneContext.Types.Skip(number).ToList();
    }
}
