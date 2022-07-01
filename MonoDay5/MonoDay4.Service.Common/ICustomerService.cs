using MonoDay4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Service.Common
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomer();
        Customer GetCustomer(Guid customerId);
        Customer Post(Customer customer);
        Customer Put(Guid customerId, Customer customer);
        bool Delete(Guid customerId);
    }
}
