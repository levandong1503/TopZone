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
		private readonly IUnitOfWork _unitOfWork;
		public TypeProductService(IUnitOfWork unitOfWork) 
        {
			_unitOfWork = unitOfWork;
        }

		public int AddTypeProducts(IEnumerable<TypeProduct> typeProducts)
		    => _unitOfWork.TypeProductRepository.AddRange(typeProducts);

		public IEnumerable<TypeProduct> GetMulti(IEnumerable<int> ids)
            => _unitOfWork.TypeProductRepository.GetMulti(tp => ids.Contains(tp.IdProduct));

	}
}
