using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;
using Talabat.Repositries.BasketRepositry;
using Talabat.Route.APIs.DTOS;
using Talabat.Route.APIs.Errors;

namespace Talabat.Route.APIs.Controllers
{

	public class BasketController : BaseApiController
	{
		private readonly IBasketRepositry _basketRepositry;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepositry basketRepositry,
			IMapper mapper	)
		{
			_basketRepositry = basketRepositry;
			_mapper = mapper;
		}

		[HttpGet/*("{id}")*/] // GET: /api/basket/id
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket = await _basketRepositry.GetBasketAsync(id);

			return Ok(basket ?? new CustomerBasket(id));
		}


		[HttpPost] // POST: /api/basket

		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
		{
			var mappedBasket= _mapper.Map<CustomerBasketDto,CustomerBasket>(basket);


			var createdOrUpdatedBasket = await _basketRepositry.UpdateBasketAsync(mappedBasket);
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
