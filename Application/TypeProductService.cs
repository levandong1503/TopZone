using Application.Interface;
using Domain.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    internal class TypeProductService : ITypeProductService
    {
        private readonly ITypeProductRepository repository;
        public TypeProductService(ITypeProductRepository typeProductRepository) 
        {
            repository = typeProductRepository;
        }

		public int AddTypeProducts(IEnumerable<TypeProduct> typeProducts)
		    => repository.AddRange(typeProducts);

		public IEnumerable<TypeProduct> GetMulti(IEnumerable<int> ids)
            => repository.GetMulti(tp => ids.Contains(tp.IdProduct));

	}
}
