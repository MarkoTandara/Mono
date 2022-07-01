using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoDay4.WebApi.Controllers
{
    public class RestOrder
    {
        public Guid CustomerID { get; set; }
        public string OrderName { get; set; }
    }
}