using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Core.Services.Contract
{
	public interface IProductService
	{
		Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productParams);
		Task<Product?> GetProductAsync(int id);
		Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
		Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();
		Task<int> GetCountAsync(ProductSpecParams productParams);
	}
}
