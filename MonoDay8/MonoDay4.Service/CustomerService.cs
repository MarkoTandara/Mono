using MonoDay4.Common;
using MonoDay4.Model;
using MonoDay4.Repository;
using MonoDay4.Repository.Common;
using MonoDay4.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Service
{
    public class CustomerService : ICustomerService
    {
        ICustomerRepository customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public async Task<List<Customer>> FindCustomerAsync(Paging paging, Sorting sorting, FilterCustomer filterCustomer)
        {
            return await customerRepository.FindCustomerAsync(paging, sorting, filterCustomer);
        }
        public async Task<Customer> GetCustomerAsync(Guid customerId)
        {
            return await customerRepository.GetCustomerAsync(customerId);
        }
        public async Task<Customer> PostAsync(Customer customer)
        {
            return await customerRepository.PostAsync(customer);
        }
        public async Task<Customer> PutAsync(Guid customerId, Customer customer)
        {
            return await customerRepository.PutAsync(customerId, customer);
        }
        public async Task<bool> DeleteAsync(Guid customerId)
        {
            return await customerRepository.DeleteAsync(customerId);
        }
    }
}
