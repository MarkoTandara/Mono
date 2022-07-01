using MonoDay4.Model;
using MonoDay4.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MonoDay4.WebApi.Controllers
{
    public class OrdersController : ApiController
    {
        [HttpGet]
        [Route("get-all-orders")]
        public HttpResponseMessage GetAll()
        {
            List<Order> orders = new List<Order>();
            OrderService order = new OrderService();
            orders = order.GetAllOrders();

            if (orders.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, orders);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No objects in list!");
            }
        }

        [HttpGet]
        [Route("get-order")]
        public HttpResponseMessage Get([FromUri] Guid orderId)
        {
            OrderService orderService = new OrderService();
            var order = orderService.GetOrder(orderId);
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
        public HttpResponseMessage PostOrder([FromUri] Guid customerId, [FromBody] Order order)
        {
            OrderService orderService = new OrderService();
            var result = orderService.PostOrder(customerId, order);
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
        public HttpResponseMessage Put([FromUri] Guid orderId, [FromBody] Order order)
        {
            OrderService orderService = new OrderService();
            var result = orderService.PutOrder(orderId, order);
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
        public HttpResponseMessage DeleteOrder([FromUri] Guid orderId)
        {
            OrderService orderService = new OrderService();
            var order = orderService.DeleteOrder(orderId);
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
