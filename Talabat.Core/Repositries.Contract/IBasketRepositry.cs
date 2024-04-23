using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositries.Contract
{
	public interface IBasketRepositry
	{
		Task<CustomerBasket?> GetBasketAsync(string busketId);
		Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket);
		Task<bool> DeleteBasketAsync(string busketId);
	}
}
