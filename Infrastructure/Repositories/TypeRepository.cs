using Domain.Abstractions;
using Domain.Exceptions;
using Infrastructure.Data;
using Type = Domain.Entities.Type;

namespace Infrastructure.Repositories;

public class TypeRepository : RepositoryBase<Type>, ITypeRepository
{
    private readonly TopZoneContext _topZoneContext;
    public TypeRepository(TopZoneContext topZoneContext) : base(topZoneContext)
    {
        _topZoneContext = topZoneContext;
    }

    public override Type Add(Type type)
    {
        var result = _topZoneContext.Add(type).Entity;
        _topZoneContext.SaveChanges();
        return result;
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

    public IEnumerable<Type> GetNumberOfTypes(int numberOfTypes)
    {
        return _topZoneContext.Types.Take(numberOfTypes).ToList();
    }
}
