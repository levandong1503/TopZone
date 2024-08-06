using Domain.Entities;

namespace Domain.Abstractions;

public interface ITypeProductRepository : IRepositoryBase<TypeProduct>
{
    public int AddRange(IEnumerable<TypeProduct> typeProducts);
}
