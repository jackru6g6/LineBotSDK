using LineBotSDK.Service.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LineBotSDK.Controllers
{
    public class OrderAPIController : ApiController
    {
        private OrderService _orderService;

        [HttpGet]
        public IHttpActionResult GetOrder(string uid, DateTime? date = null)
        {
            if (string.IsNullOrWhiteSpace(uid))
            {
                return Ok();
            }

            _orderService = new OrderService(uid);
            date = date ?? DateTime.Now;

            return Ok(_orderService.GetAllOrderByDate(date.Value));
        }

        [HttpDelete]
        public IHttpActionResult Order(string uid, string restaurant, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(uid))
            {
                return Ok();
            }

            new OrderService(uid).DeleteOrder(new OrderService.DeleteOrderDto
            {
                restaurant = restaurant,
                date = date,
            });

            return Ok();
        }
    }
}