using MonoDay4.Model;
using MonoDay4.Repository;
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
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orderRepository = new OrderRepository();
            return await orderRepository.GetAllOrdersAsync();
        }
        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            var orderRepository = new OrderRepository();
            return await orderRepository.GetOrderAsync(orderId);
        }
        public async Task<Order> PutOrderAsync(Guid orderId, Order order)
        {
            var orderRepository = new OrderRepository();
            return await orderRepository.PutOrderAsync(orderId, order);
        }
        public async Task<Order> PostOrderAsync(Guid customerId, Order order)
        {
            var orderRepository = new OrderRepository();
            return await orderRepository.PostOrderAsync(customerId, order);
        }
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var orderRepository = new OrderRepository();
            return await orderRepository.DeleteOrderAsync(orderId);
        }
    }
}
