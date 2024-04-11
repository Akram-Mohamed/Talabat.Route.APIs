using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repositries.Data
{
	public static class StoredContextSeed
	{


		public async static Task SeedAsync(StoreContext _dbContext)
		{


			if (_dbContext.ProductBrands.Count() == 0)
			{
				var brandsData = File.ReadAllText("../Talabat.Repositries.Data/brands.json");

				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
				if (brands?.Count() > 0)
				{
					foreach (var brand in brands)
					{

						_dbContext.Set<ProductBrand>().Add(brand);
					}
					await _dbContext.SaveChangesAsync();
				}
			}

			if (_dbContext.ProductCategories.Count() == 0)
			{
				var CategoryData = File.ReadAllText("../Talabat.Repositries.Data/brands.json");

				var Categories= JsonSerializer.Deserialize<List<ProductBrand>>(CategoryData);
				if (Categories?.Count() > 0)
				{
					foreach (var brand in Categories)
					{

						_dbContext.Set<ProductBrand>().Add(brand);
					}
					await _dbContext.SaveChangesAsync();
				}
			}


		}

	}


}
