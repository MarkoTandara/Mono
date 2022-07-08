using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoDay4.WebApi.Controllers
{
    public class RestOrder
    {
        public Guid CustomerId { get; set; }
        public string OrderName { get; set; }

        public RestOrder( Guid customerId, string orderName)
        {
            this.CustomerId = customerId;
            this.OrderName = orderName;
        }
    }
}