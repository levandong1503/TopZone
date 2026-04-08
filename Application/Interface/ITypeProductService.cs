using Domain.Entities;

namespace Application.Interface;

public interface ITypeProductService
{
    public int AddTypeProducts(IEnumerable<TypeProduct> typeProducts);
}
