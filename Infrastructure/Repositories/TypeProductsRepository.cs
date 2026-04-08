using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class TypeProductsRepository : RepositoryBase<TypeProduct>, ITypeProductRepository
    {
        public TypeProductsRepository(TopZoneContext topZoneContext) : base(topZoneContext) { }

        public int AddRange(IEnumerable<TypeProduct> typeProducts)
        {
            DbContext.AddRange(typeProducts);

            return DbContext.SaveChanges();
        }
    }
}
