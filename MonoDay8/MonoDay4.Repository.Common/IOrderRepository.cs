using MonoDay4.Common;
using MonoDay4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Repository.Common
{
    public interface IOrderRepository
    {
        Task<List<Order>> FindOrderAsync(Paging paging, Sorting sorting, FilterOrder filterOrder);
        Task<Order> GetOrderAsync(Guid orderId);
        Task<Order> PostOrderAsync(Guid customerId, Order order);
        Task<Order> PutOrderAsync(Guid orderId, Order order);
        Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
