using Talabat.Core.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;


namespace Talabat.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<Entities.Order_Aggregate.Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, ShippingAddress shippingAddress);
        Task<IReadOnlyList<Entities.Order_Aggregate.Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Entities.Order_Aggregate.Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId);
        Task<IReadOnlyList<DeliveryMethod>> GetDelivreyMethodsAsync();
    }
}
