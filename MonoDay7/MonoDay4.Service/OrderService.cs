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
        IOrderRepository iOrderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this.iOrderRepository = orderRepository;
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await iOrderRepository.GetAllOrdersAsync();
        }
        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            return await iOrderRepository.GetOrderAsync(orderId);
        }
        public async Task<Order> PutOrderAsync(Guid orderId, Order order)
        {
            return await iOrderRepository.PutOrderAsync(orderId, order);
        }
        public async Task<Order> PostOrderAsync(Guid customerId, Order order)
        {
            return await iOrderRepository.PostOrderAsync(customerId, order);
        }
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            return await iOrderRepository.DeleteOrderAsync(orderId);
        }
    }
}
