using Talabat.Core.Entities.Identity;

namespace Talabat.Route.APIs.DTOS
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; } = null!;

        public DateTimeOffset OrderDate { get; set; }

        public string Status { get; set; }

        public Address ShippingAddress { get; set; }

        public string DeliveyMethod { get; set; }
        public decimal DeliveyMethodCoast { get; set; }

        public ICollection<OrderItemDTO> Items { get; set; } = new HashSet<OrderItemDTO>();

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public string PaymentIntentId { get; set; } = String.Empty;

    }
}
