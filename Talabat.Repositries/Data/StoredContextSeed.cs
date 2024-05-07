using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repositries.Data
{
	public static class StoredContextSeed
	{


		public async static Task SeedAsync(StoreContext _dbContext)
		{



            if (_dbContext.ProductBrands.Count() == 0)
			{
                var BrandsJson = File.ReadAllText("../Infrastructure/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsJson);
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
            if (_dbContext.Products.Count() == 0)
            {
                var ProductsJson = File.ReadAllText("../Infrastructure/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsJson);

                foreach (var product in Products)
                {
                    _dbContext.Products.Add(product);
                }
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.DelivreyMethods.Any())
            {
                var deliveryMethodJson = File.ReadAllText("../Infrastructure/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodJson);

                if (deliveryMethods?.Count > 0)
                {
                    foreach (var item in deliveryMethods)
                    {
                        _dbContext.DelivreyMethods.Add(item);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }

        }

	}


}
