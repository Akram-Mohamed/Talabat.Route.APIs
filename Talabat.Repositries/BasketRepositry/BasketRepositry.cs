using StackExchange.Redis;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;

namespace Talabat.Repositries.BasketRepositry
{
	public class BasketRepositry : IBasketRepositry
	{
		private readonly IDatabase _database;
		public BasketRepositry(IConnectionMultiplexer Redis)
		{
			_database = Redis.GetDatabase();

		}

		public async Task<bool> DeleteBasketAsync(string busketId)
		{
			return await _database.KeyDeleteAsync(busketId);
		}

		public async Task<CustomerBasket?> GetBasketAsync(string busketId)
		{
			var basket = await _database.StringGetAsync(busketId);

			return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket)
		{
			var createdOrUpdated = await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize<CustomerBasket>(customerBasket), TimeSpan.FromDays(20));
			if (createdOrUpdated is false) { return null; }
			return await GetBasketAsync(customerBasket.Id);

		}



	}
}
