using System.ComponentModel.DataAnnotations;

namespace Talabat.Route.APIs.DTOS
{
	public class BasketItemDto
	{

		[Required]
		public int Id { get; set; }

		[Required]
		public string ProductName { get; set; }

		[Required]
		public string PictureUrl { get; set; }

		[Required]
		public string Category { get; set; }

		[Required]
		public string Brand { get; set; }

		[Required]
		[Range(0.1, double.MaxValue, ErrorMessage = "Price Should be more than Zero!!!!!!")]
		public decimal Price { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity Should be at least One")]
		public int Quantity { get; set; }
	}
}