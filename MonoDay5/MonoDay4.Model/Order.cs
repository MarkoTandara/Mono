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
        public Guid OrderID { get; set; }
        public Guid CustomerID { get; set; }
        public string OrderName { get; set; }

        public Order(Guid orderID, Guid customerID, string orderName)
        {
            this.OrderID = orderID;
            this.CustomerID = customerID;
            this.OrderName = orderName;
        }
        public Order()
        {

        }
    }
}
