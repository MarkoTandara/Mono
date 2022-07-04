using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Model.Common
{
    public interface ICustomer
    {
        Guid CustomerId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
