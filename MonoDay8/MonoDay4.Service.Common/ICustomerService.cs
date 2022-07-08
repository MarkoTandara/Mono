using MonoDay4.Common;
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
        Task<List<Customer>> FindCustomerAsync(Paging paging, Sorting sorting, FilterCustomer filterCustomer);
        Task<Customer> GetCustomerAsync(Guid customerId);
        Task<Customer> PostAsync(Customer customer);
        Task<Customer> PutAsync(Guid customerId, Customer customer);
        Task<bool> DeleteAsync(Guid customerId);
    }
}
