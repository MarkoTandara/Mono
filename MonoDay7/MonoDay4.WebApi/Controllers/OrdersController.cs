using MonoDay4.Model;
using MonoDay4.Service;
using MonoDay4.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MonoDay4.WebApi.Controllers
{
    public class OrdersController : ApiController
    {
        IOrderService iCustomerService;
        public OrdersController(IOrderService orderService)
        {
            this.iCustomerService = orderService;
        }

        [HttpGet]
        [Route("get-all-orders")]
        public async Task<HttpResponseMessage> GetAllAsync()
        {
            List<Order> orders = new List<Order>();
            List<RestOrder> ordersRest = new List<RestOrder>();
            orders = await iCustomerService.GetAllOrdersAsync();

            if (orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    ordersRest.Add(new RestOrder(order.CustomerId, order.OrderName));
                }
                return Request.CreateResponse(HttpStatusCode.OK, ordersRest);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No objects in list!");
            }
        }

        [HttpGet]
        [Route("get-order")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Guid orderId)
        {
            var order = await iCustomerService.GetOrderAsync(orderId);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, order);
            }
        }

        [HttpPost]
        [Route("post-order")]
        public async Task<HttpResponseMessage> PostAsync([FromUri] Guid customerId, [FromBody] Order order)
        {
            var result = await iCustomerService.PostOrderAsync(customerId, order);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, order);
            }

        }

        [HttpPut]
        [Route("put-order")]
        public async Task<HttpResponseMessage> PutAsync([FromUri] Guid orderId, [FromBody] Order order)
        {
            var result = await iCustomerService.PutOrderAsync(orderId, order);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Customer updated!");
            }
        }

        [HttpDelete]
        [Route("delete-order")]
        public async Task<HttpResponseMessage> DeleteAsync([FromUri] Guid orderId)
        {
            var order = await iCustomerService.DeleteOrderAsync(orderId);
            if (order == false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Order deleted!");
            }
        }
    }
}
