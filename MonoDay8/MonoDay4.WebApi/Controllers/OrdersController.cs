using MonoDay4.Common;
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
        IOrderService customerService;
        public OrdersController(IOrderService orderService)
        {
            this.customerService = orderService;
        }

        [HttpGet]
        [Route("get-all-orders")]
        public async Task<HttpResponseMessage> FindAsync(Guid? customerId, string orderName, int pageNumber= 1, int recordsBy = 10, string orderBy = "FirstName", string sortOrder = "asc")
        {
            var paging = new Paging(pageNumber, recordsBy);
            var sorting = new Sorting(orderBy, sortOrder);
            var filterOrder = new FilterOrder(customerId, orderName);
            List<Order> orders = new List<Order>();
            List<RestOrder> ordersRest = new List<RestOrder>();
            orders = await customerService.FindOrderAsync(paging, sorting, filterOrder);

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
            var order = await customerService.GetOrderAsync(orderId);
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
        public async Task<HttpResponseMessage> PostAsync([FromUri] Guid customerId, [FromBody] RestOrder restOrder)
        {
            var order = new Order(customerId, restOrder.OrderName);
            var result = await customerService.PostOrderAsync(customerId, order);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, restOrder);
            }

        }

        [HttpPut]
        [Route("put-order")]
        public async Task<HttpResponseMessage> PutAsync([FromUri] Guid orderId, [FromBody] Order order)
        {
            var result = await customerService.PutOrderAsync(orderId, order);
            var restOrder = new RestOrder(result.CustomerId, result.OrderName);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, restOrder);
            }
        }

        [HttpDelete]
        [Route("delete-order")]
        public async Task<HttpResponseMessage> DeleteAsync([FromUri] Guid orderId)
        {
            var order = await customerService.DeleteOrderAsync(orderId);
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
