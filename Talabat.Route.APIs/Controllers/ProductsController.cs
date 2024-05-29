using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;
using Talabat.Repositries;
using Talabat.Route.APIs.Errors;
using Talabat.Route.APIs.Helpers;

namespace Talabat.Route.APIs.Controllers
{

	public class ProductsController : BaseApiController
	{


		private readonly IGenericRepositry<Product> _productsRepo;
		private readonly IGenericRepositry<ProductBrand> _brandsRepo;
		private readonly IGenericRepositry<ProductCategory> _categoriesRepo;
		private readonly Mapper _mapper;

		public ProductsController(IGenericRepositry<Product> productsRepo,
			IGenericRepositry<ProductBrand> brandsRepo,
			IGenericRepositry<ProductCategory> categoriesRepo,
			Mapper mapper)
		{

			_productsRepo = productsRepo;
			_brandsRepo = brandsRepo;
			_categoriesRepo = categoriesRepo;
			_mapper = mapper;
		}
		// /api/
		[HttpGet]
		public async Task<IActionResult> GetProductss()
		{
			var products = await _productsRepo.GetAllAsync();
			return Ok(products);
		}


		// /api/Products/1 

		//[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status2000K)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);

			var product = await _productsRepo.GetWithSpecAsync(spec);

			if (product == null)
				return NotFound(new ApiResponse(404));

			return Ok(product);
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
		public async Task<ActionResult<Pagination<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(specParams);
			var products = await _productsRepo.GetAllWithSpecAsync(spec);
			var countSpec = new ProductWithFilterationForCount(specParams);
			var count= await _productsRepo.GetCountAsync(countSpec);
			//var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
			return Ok(new Pagination<Product>(specParams.PageIndex, count, specParams.PageSize, products));
		}


		[HttpGet("brands")] // GET: /api/products/brands
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _brandsRepo.GetAllAsync();
			return Ok(brands);
		}


		[HttpGet("categories")] // GET: /api/products/categories
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
		{
			var categories = await _categoriesRepo.GetAllAsync();
			return Ok(categories);


		}
	}
}
