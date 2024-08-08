using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
	public class TypeRequest
	{
		public required string Name { get; set; }
		public int? MainTypeId { get; set; }
	}
}
