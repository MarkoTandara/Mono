using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Common
{
    public class FilterOrder
    {   
        public Guid? CustomerId { get; set; }
        public string OrderName { get; set; }

        public FilterOrder(Guid? customerId, string orderName)
        {
            this.CustomerId = customerId;
            this.OrderName = orderName;
        }
        public FilterOrder()
        {

        }
    }
}
