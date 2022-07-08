using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Model.Common
{
    public interface IOrder
    {
        Guid OrderID { get; set; }
        Guid CustomerID { get; set; }
        string OrderName { get; set; }
    }
}
