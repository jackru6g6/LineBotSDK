using LineBotSDK.Models.Views.Order;
using LineBotSDK.Service.Menu;
using LineBotSDK.Service.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LineBotSDK.Controllers
{
    public class OrderController : Controller
    {
        private MenuService _menuService;
        private OrderService _orderService;

        public OrderController()
        {
            _menuService = new MenuService();
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetOrder(string uid, DateTime? date = null)
        {
            if (string.IsNullOrWhiteSpace(uid))
            {
                return Json(null);
            }

            _orderService = new OrderService(uid);
            date = date ?? DateTime.Now;

            var a = _orderService.GetAllOrderByDate(date.Value);

            return Json(_orderService.GetAllOrderByDate(date.Value), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DeleteOrder(string uid, string restaurant, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(uid))
            {
                //return Json(null);
                return;
            }

            new OrderService(uid).DeleteOrder(new OrderService.DeleteOrderDto
            {
                restaurant = restaurant,
                date = date,
            });
            //return ok();
            //return Json(_orderService.GetAllOrderByDate(date.Value), JsonRequestBehavior.AllowGet);
        }

    }
}