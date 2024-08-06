using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TypeProductsRepository : RepositoryBase<TypeProduct>, ITypeProductRepository
    {
        protected readonly TopZoneContext DbContext;
        public TypeProductsRepository(TopZoneContext topZoneContext) : base(topZoneContext)
        {
            topZoneContext = DbContext;
        }

        public int AddRange(IEnumerable<TypeProduct> typeProducts)
        {
            DbContext.AddRange(typeProducts);

            return DbContext.SaveChanges();
        }
    }
}
