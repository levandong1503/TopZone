using System.Linq.Expressions;

namespace Domain.Abstractions;

public interface IRepositoryBase<T> where T : class
{
    T GetById(int id);
    T Add(T entity);
    T Delete(int id);
    IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);
    IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 20, string[] includes = null);
}
