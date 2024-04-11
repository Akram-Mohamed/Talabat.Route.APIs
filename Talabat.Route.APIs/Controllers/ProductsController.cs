using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;

namespace Talabat.Route.APIs.Controllers
{

	public class ProductsController : BaseApiController
	{



		public ProductsController(IGenericRepositry<Product> productsRepo)
		{

		}

	}
}
