using MonoDay4.Common;
using MonoDay4.Model;
using MonoDay4.Repository;
using MonoDay4.Repository.Common;
using MonoDay4.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Service
{
    public class OrderService : IOrderService
    {
        IOrderRepository orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<List<Order>> FindOrderAsync(Paging paging, Sorting sorting, FilterOrder filterOrder)
        {
            return await orderRepository.FindOrderAsync(paging, sorting, filterOrder);
        }
        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            return await orderRepository.GetOrderAsync(orderId);
        }
        public async Task<Order> PutOrderAsync(Guid orderId, Order order)
        {
            return await orderRepository.PutOrderAsync(orderId, order);
        }
        public async Task<Order> PostOrderAsync(Guid customerId, Order order)
        {
            return await orderRepository.PostOrderAsync(customerId, order);
        }
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            return await orderRepository.DeleteOrderAsync(orderId);
        }
    }
}
