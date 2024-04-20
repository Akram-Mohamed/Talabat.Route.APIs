using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;


namespace Talabat.Core.Specifications.Product_Specs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
	{

		// This Constructor will be Used for Creating an Object, That will be Used to Get All Products O references
		public ProductWithBrandAndCategorySpecifications(string sort, int? brandId, int? categoryId)
			: base(P =>

						(!brandId.HasValue || P.BrandId == brandId.Value) &&
						(!categoryId.HasValue || P.CategoryId == categoryId.Value)

			)
		{
			//Includes.Add(P => P.Brand);
			//Includes.Add(P => P.Category);
			AddIncludes();
			AddOrderBy(P => P.Name);
			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{

					case "priceAsc":
						//OrderBy = P => P.Price;
						AddOrderBy(P => P.Price);
						break;
					case "priceDesc":
						//OrderByDesc= P => P.Price;
						AddOrderByDesc(P => P.Price); break;
					default:
						AddOrderBy(P => P.Name);
						break;

				}
			}
		}



		public ProductWithBrandAndCategorySpecifications(int id)
			: base(P => P.Id == id)
		{
			AddIncludes();
		}


		private void AddIncludes()
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);




		}
	}
}
