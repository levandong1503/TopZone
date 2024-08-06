using Domain.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly DbSet<T> dbSet;

    protected readonly TopZoneContext DbContext;

    protected RepositoryBase(TopZoneContext topZoneContext)
    {
        DbContext = topZoneContext;
        dbSet = DbContext.Set<T>();
    }

    public virtual T GetById(int id)
    {
        var result = dbSet.Find(id);
        if (result == null)
        {
            throw new KeyNotFoundException();
        }    

        return result;
    }

    public virtual T Add(T entity)
    {
        return dbSet.Add(entity).Entity;
    }

    public virtual T Delete(int id)
    {
        var entity = dbSet.Find(id);
        if (entity == null)
        {
            throw new KeyNotFoundException();
        }
        return dbSet.Remove(entity).Entity;
    }

    public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
    {
        //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
        if (includes != null && includes.Count() > 0)
        {
            var query = dbSet.Include(includes.First());
            foreach (var include in includes.Skip(1))
                query = query.Include(include);
            return query.Where<T>(predicate).AsQueryable<T>();
        }

        return dbSet.Where<T>(predicate).AsQueryable<T>();
    }

    public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 20, string[] includes = null)
    {
        int skipCount = index * size;
        IQueryable<T> _resetSet;

        //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
        if (includes != null && includes.Count() > 0)
        {
            var query = dbSet.Include(includes.First());
            foreach (var include in includes.Skip(1))
                query = query.Include(include);
            _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
        }
        else
        {
            _resetSet = predicate != null ? dbSet.Where<T>(predicate).AsQueryable() : dbSet.AsQueryable();
        }

        _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
        total = _resetSet.Count();
        return _resetSet.AsQueryable();
    }
}
