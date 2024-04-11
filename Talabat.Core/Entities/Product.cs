using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
	public class Product : BaseEntitiy
	{


		public string Name { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public decimal Price { get; set; }

		//[ForeignKey(nameof (Product.Brand))]
		public int BrandId { get; set; } //Forieng Key Column=> Product Brand
		//[InverseProperty (nameof (ProductBrand. Products))]
		public ProductBrand Brand { get; set; }//Navigation Property [ONE]

		//[ForeignKey(nameof (Product.Category))]
		public int CategoryId { get; set; } //Forieng Key Column=> Product Category
		public ProductCategory Category { get; set; }//Navigation Property [ONE]







	}
}
