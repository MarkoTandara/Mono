using MonoDay4.Model;
using MonoDay4.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MonoDay4.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll()
        {
            List<Customer> customers = new List<Customer>();
            CustomerService customer = new CustomerService();
            customers = customer.GetAllCustomer();

            if (customers.Count>0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, customers);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No objects in list!");
            }
        }

        [HttpGet]
        [Route("get")]
        public HttpResponseMessage Get([FromUri] Guid customerId)
        {
            CustomerService customerService = new CustomerService();
            var customer = customerService.GetCustomer(customerId);
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
        [Route("post")]
        public HttpResponseMessage Post([FromBody] Customer customer)
        {
            CustomerService customerService = new CustomerService();
            var result = customerService.Post(customer);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, customerService);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
            
        }

        [HttpPut]
        [Route("put")]
        public HttpResponseMessage Put([FromUri] Guid customerId, [FromBody] Customer customer)
        {
            CustomerService customerService = new CustomerService();
            var result = customerService.GetCustomer(customerId);
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
        [Route("delete")]
        public HttpResponseMessage Delete([FromUri] Guid customerId)
        {
            CustomerService customerService = new CustomerService();
            var customer = customerService.Delete(customerId);
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
