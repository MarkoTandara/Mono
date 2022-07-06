using MonoDay4.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Model
{
    public class Order : IOrder
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string OrderName { get; set; }

        public Order(Guid orderId, Guid customerId, string orderName)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
            this.OrderName = orderName;
        }
        public Order(Guid customerId, string orderName)
        {
            this.CustomerId = customerId;
            this.OrderName = orderName;
        }
        public Order()
        {

        }
    }
}
