using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;
using Talabat.Repositries.BasketRepositry;
using Talabat.Route.APIs.Errors;

namespace Talabat.Route.APIs.Controllers
{

	public class BasketController : BaseApiController
	{
		private readonly IBasketRepositry _basketRepositry;

		public BasketController(IBasketRepositry basketRepositry)
		{
			_basketRepositry = basketRepositry;
		}

		[HttpGet/*("{id}")*/] // GET: /api/basket/id
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket = await _basketRepositry.GetBasketAsync(id);

			return Ok(basket ?? new CustomerBasket(id));
		}


		[HttpPost] // POST: /api/basket

		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
		{
			var createdOrUpdatedBasket = await _basketRepositry.UpdateBasketAsync(basket);
			if (createdOrUpdatedBasket != null) 
				{ return BadRequest(new ApiResponse(400)) ; }

			return Ok(createdOrUpdatedBasket	);
		}

		[HttpDelete]
		public async Task DeleteBasket(string id)
		{
			await _basketRepositry.DeleteBasketAsync(id);
		}

	}
}
