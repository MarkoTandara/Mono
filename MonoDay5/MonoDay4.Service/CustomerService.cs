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
        public List<Customer> GetAllCustomer()
        {
            var customerRepository = new CustomerRepository();
            return customerRepository.GetAllCustomer();
        }
        public Customer GetCustomer(Guid customerId)
        {
            var customerRepository = new CustomerRepository();
            return customerRepository.GetCustomer(customerId);
        }
        public Customer Post(Customer customer)
        {
            var customerRepository = new CustomerRepository();
            return customerRepository.Post(customer);
        }
        public Customer Put(Guid customerId, Customer customer)
        {
            var customerRepository = new CustomerRepository();
            return customerRepository.Put(customerId, customer);
        }
        public bool Delete(Guid customerId)
        {
            var customerRepository = new CustomerRepository();
            return customerRepository.Delete(customerId);
        }
    }
}
