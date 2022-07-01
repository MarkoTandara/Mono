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
        public List<Order> GetAllOrders()
        {
            var orderRepository = new OrderRepository();
            return orderRepository.GetAllOrders();
        }
        public Order GetOrder(Guid orderId)
        {
            var orderRepository = new OrderRepository();
            return orderRepository.GetOrder(orderId);
        }
        public Order PutOrder(Guid orderId, Order order)
        {
            var orderRepository = new OrderRepository();
            return orderRepository.PutOrder(orderId, order);
        }
        public Order PostOrder(Guid orderId, Order order)
        {
            var orderRepository = new OrderRepository();
            return orderRepository.PostOrder(orderId, order);
        }
        public bool DeleteOrder(Guid orderId)
        {
            var orderRepository = new OrderRepository();
            return orderRepository.DeleteOrder(orderId);
        }
    }
}
