using Domain.Entities;
using Microsoft.Identity.Client;

namespace Application.Interface;

public interface ITypeProductService
{
    public int AddTypeProducts(IEnumerable<TypeProduct> typeProducts);
}
