﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contract;
using Talabat.Core.specifications.OrderSpecs;
using Talabat.Core;
using Talabat.Core.Repositries.Contract;
using Talabat.Core.Entities;

namespace Talabat.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepositry _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(
            IBasketRepositry basketRepo,
            IUnitOfWork unitOfWork
            
            )
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, ShippingAddress shippingAddress)
        {
            //1- get basket form basket repo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            //2- get selected items at basket form products repo 
            List<OrderItem> orderItems = new List<OrderItem>();

            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    var productItemOrdered = new ProductItemOrdered(item.Id, product?.Name ?? String.Empty, product?.PictureUrl ?? String.Empty);

                    var orderItem = new OrderItem(productItemOrdered, item.Quantity, product.Price);
                    orderItems.Add(orderItem);
                }
            }
            //3- claculate sub-total
            decimal subTotal = orderItems.Sum(item => item.Quantity * item.Price);

            //4- get deliveryMethod from deliveryMethods repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);
            //5- create order
            Order order = new Order()
            {
                BuyerEmail = buyerEmail,
                DelivreyMethod = deliveryMethod,
                ShippingAddress = shippingAddress,
                SubTotal = subTotal,
                Items = orderItems,
            };
            _unitOfWork.Repository<Order>().Add(order);

            //6- save To Database
            var result = await _unitOfWork.Compelete();

            if (result <= 0) return null;

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDelivreyMethodsAsync()
            => await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

        public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            var ordersRepos = _unitOfWork.Repository<Order>();
            var orderSpecifications = new OrderSpecifications(buyerEmail, orderId);
            var order = await ordersRepos.GetWithSpecAsync(orderSpecifications);
            return order;
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var ordersRepos = _unitOfWork.Repository<Order>();
            var orderSpecifications = new OrderSpecifications(buyerEmail);
            var orders = ordersRepos.GetAllWithSpecAsync(orderSpecifications);
            return orders;
        }
    }

}
