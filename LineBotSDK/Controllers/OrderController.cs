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
            var data = _orderService.GetAllOrderByDate(date.Value);

            DetailsViewModel aaa = new DetailsViewModel();
            aaa.RestaurantName = "蜂鳥食堂";
            aaa.ChooseDate = date.Value;
            aaa.Orders = data.Select(t => new DetailsViewModel.Order
            {
                DishName = t.meal,
            }).ToList();

            return Json(aaa, JsonRequestBehavior.AllowGet);
        }
    }
}