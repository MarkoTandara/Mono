using MonoDay4.Model;
using MonoDay4.Service;
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
        [HttpGet]
        [Route("get-all-orders")]
        public async Task<HttpResponseMessage> GetAllAsync()
        {
            List<Order> orders = new List<Order>();
            List<RestOrder> ordersRest = new List<RestOrder>();
            OrderService orderService = new OrderService();
            orders = await orderService.GetAllOrdersAsync();

            if (orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    ordersRest.Add(new RestOrder(order.CustomerId, order.OrderName));
                }
                return Request.CreateResponse(HttpStatusCode.OK, orders);
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
            OrderService orderService = new OrderService();
            var order = await orderService.GetOrderAsync(orderId);
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
            OrderService orderService = new OrderService();
            var result = await orderService.PostOrderAsync(customerId, order);
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
            OrderService orderService = new OrderService();
            var result = await orderService.PutOrderAsync(orderId, order);
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
            OrderService orderService = new OrderService();
            var order = await orderService.DeleteOrderAsync(orderId);
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
