﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;
using Talabat.Repositries;

namespace Talabat.Route.APIs.Controllers
{

	public class ProductsController : BaseApiController
	{


		private readonly IGenericRepositry<Product> _productsRepo;

		public ProductsController(IGenericRepositry<Product> productsRepo)
		{
			_productsRepo = productsRepo;
		}
		// /api/
		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			var products = await _productsRepo.GetAllAsync();
			return Ok(products);
		}

		// /api/Products/1 [HttpGet("{id}")]

		//public async Task<ActionResult<Product>> GetProduct(int id)
		//{
		//	var product = await _productsRepo.GetAsync(id);
		//	if (product is null)
		//		return NotFound(); 
		//	return Ok(product);
		//}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
		{
			var spec = new ProductWithBrandAndCategorySpecifications();
			var products = await _productsRepo.GetAllWithSpecAsync(spec);
			return Ok(products);
		}
	}
}
