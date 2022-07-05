using MonoDay4.Model;
using MonoDay4.Service;
using MonoDay4.Service.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MonoDay4.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        ICustomerService iCustomerService;
        public CustomerController(ICustomerService customerService)
        {
            this.iCustomerService = customerService;
        }

        [HttpGet]
        [Route("get-all-customers")]
        public async Task<HttpResponseMessage> GetAllAsync()
        {
            List<Customer> customers = new List<Customer>();
            List<RestCustomer> customersRest = new List<RestCustomer>();
            customers = await iCustomerService.GetAllCustomerAsync();
            if (customers.Count>0)
            {
                foreach(Customer customer in customers)
                {
                    customersRest.Add(new RestCustomer(customer.FirstName, customer.LastName));
                }
                return Request.CreateResponse(HttpStatusCode.OK, customers);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No objects in list!");
            }
        }

        [HttpGet]
        [Route("get-customer")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Guid customerId)
        {
            var customer = await iCustomerService.GetCustomerAsync(customerId);
            if(customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
        }

        [HttpPost]
        [Route("post-customer")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] Customer customer)
        {
            var result = await iCustomerService.PostAsync(customer);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, iCustomerService);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }            
        }

        [HttpPut]
        [Route("put-customer")]
        public async Task<HttpResponseMessage> PutAsync([FromUri] Guid customerId, [FromBody] Customer customer)
        {
            var result = await iCustomerService.PutAsync(customerId, customer);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Customer updated!");
            }
        }

        [HttpDelete]
        [Route("delete-customer")]
        public async Task<HttpResponseMessage> DeleteAsync([FromUri] Guid customerId)
        {
            var customer = await iCustomerService.DeleteAsync(customerId);
            if (customer == false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Customer deleted!");
            }
        }
    }
}
