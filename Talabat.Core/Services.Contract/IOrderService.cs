using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, ShippingAddress shippingAddress);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId);
    Task<IReadOnlyList<DeliveryMethod>> GetDelivreyMethodsAsync();
}