using MonoDay4.Common;
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
        ICustomerService customerService;
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        [Route("get-all-customers")]
        public async Task<HttpResponseMessage> FindAsync(string firstName, string lastName, int pageNumber=1, int recordsBy=10, string orderBy="FirstName", string sortOrder="asc")
        {
            var paging = new Paging(pageNumber, recordsBy);
            var sorting = new Sorting(orderBy, sortOrder);
            var filterCustomer = new FilterCustomer(firstName, lastName);
            List<Customer> customers = new List<Customer>();
            List<RestCustomer> customersRest = new List<RestCustomer>();
            customers = await customerService.FindCustomerAsync(paging, sorting, filterCustomer);
            if (customers.Count>0)
            {
                foreach(Customer customer in customers)
                {
                    customersRest.Add(new RestCustomer(customer.FirstName, customer.LastName));
                }
                return Request.CreateResponse(HttpStatusCode.OK, customersRest);
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
            var customer = await customerService.GetCustomerAsync(customerId);
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
        public async Task<HttpResponseMessage> PostAsync([FromBody] RestCustomer restCustomer)
        {
            var customer = new Customer(restCustomer.FirstName, restCustomer.LastName);
            var result = await customerService.PostAsync(customer);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, customerService);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }            
        }

        [HttpPut]
        [Route("put-customer")]
        public async Task<HttpResponseMessage> PutAsync([FromUri] Guid customerId, [FromBody] RestCustomer restCustomer)
        {
            var customer = new Customer(restCustomer.FirstName, restCustomer.LastName);
            var result = await customerService.PutAsync(customerId, customer);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, restCustomer);
            }
        }

        [HttpDelete]
        [Route("delete-customer")]
        public async Task<HttpResponseMessage> DeleteAsync([FromUri] Guid customerId)
        {
            var customer = await customerService.DeleteAsync(customerId);
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
