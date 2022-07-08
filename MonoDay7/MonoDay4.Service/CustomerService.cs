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
        ICustomerRepository iCustomerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.iCustomerRepository = customerRepository;
        }
        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            return await iCustomerRepository.GetAllCustomerAsync();
        }
        public async Task<Customer> GetCustomerAsync(Guid customerId)
        {
            return await iCustomerRepository.GetCustomerAsync(customerId);
        }
        public async Task<Customer> PostAsync(Customer customer)
        {
            return await iCustomerRepository.PostAsync(customer);
        }
        public async Task<Customer> PutAsync(Guid customerId, Customer customer)
        {
            return await iCustomerRepository.PutAsync(customerId, customer);
        }
        public async Task<bool> DeleteAsync(Guid customerId)
        {
            return await iCustomerRepository.DeleteAsync(customerId);
        }
    }
}
