using MonoDay4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Service.Common
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderAsync(Guid orderId);
        Task<Order> PostOrderAsync(Guid customerId, Order order);
        Task<Order> PutOrderAsync(Guid orderId, Order order);
        Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
