﻿using System;
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
		public ProductWithBrandAndCategorySpecifications()
			: base()
		{
			//Includes.Add(P => P.Brand);
			//Includes.Add(P => P.Category);
			AddIncludes();
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