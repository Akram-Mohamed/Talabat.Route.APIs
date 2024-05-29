using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;
using Talabat.Repositries;
using Talabat.Route.APIs.DTOS;
using Talabat.Route.APIs.Errors;
using Talabat.Route.APIs.Helpers;
using Talabat.Services;

namespace Talabat.Route.APIs.Controllers
{

    public class ProductsController : BaseApiController
    {


        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(
            //IGenericRepository<Product> genericRepository,
            //IGenericRepository<ProductCategory> productCatigoriesRepo,
            //IGenericRepository<ProductBrand> productsBrandsRepo,
            IMapper mapper,
            IProductService productService
            )
        {
            //productsRepository = genericRepository;
            //_productCatigoriesRepo = productCatigoriesRepo;
            //_productsBrandsRepo = productsBrandsRepo;
            _mapper = mapper;
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProducts([FromQuery] ProductSpecParams productParams)
        {
            //var Products = await productsRepository.GetAllAsync();

            var Products = await _productService.GetProductsAsync(productParams);
            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
            var Count = await _productService.GetCountAsync(productParams);
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, Count, Data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {

            var Product = await _productService.GetProductAsync(id);
            if (Product is null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });
            return Ok(_mapper.Map<Product, ProductToReturnDto>(Product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetProductCategories()
        {
            var categories = await _productService.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}