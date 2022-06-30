using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoDay4.WebApi.Models
{
    public class Order
    {
        public string OrderID { get; set; }
        public string CustomerID { get; set; }
        public string OrderName { get; set; }

        public Order(string orderID, string customerID, string orderName)
        {
            this.OrderID = orderID;
            this.CustomerID = customerID;
            this.OrderName = orderName;
        }
    }
}