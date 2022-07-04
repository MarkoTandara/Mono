using MonoDay4.Model;
using MonoDay4.Repository;
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
        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            var customerRepository = new CustomerRepository();
            return await customerRepository.GetAllCustomerAsync();
        }
        public async Task<Customer> GetCustomerAsync(Guid customerId)
        {
            var customerRepository = new CustomerRepository();
            return await customerRepository.GetCustomerAsync(customerId);
        }
        public async Task<Customer> PostAsync(Customer customer)
        {
            var customerRepository = new CustomerRepository();
            return await customerRepository.PostAsync(customer);
        }
        public async Task<Customer> PutAsync(Guid customerId, Customer customer)
        {
            var customerRepository = new CustomerRepository();
            return await customerRepository.PutAsync(customerId, customer);
        }
        public async Task<bool> DeleteAsync(Guid customerId)
        {
            var customerRepository = new CustomerRepository();
            return await customerRepository.DeleteAsync(customerId);
        }
    }
}
