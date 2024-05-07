using System.ComponentModel.DataAnnotations;

namespace Talabat.Route.APIs.DTOS
{
    public class OrderDTO
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDTO ShippingAddress { get; set; }
    }
}
