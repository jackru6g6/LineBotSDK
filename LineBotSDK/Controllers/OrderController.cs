using LineBotSDK.Models.Views.Order;
using LineBotSDK.Service.Menu;
using LineBotSDK.Service.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static LineBotSDK.Models.Views.Order.MenuViewModel.Restaurant;

namespace LineBotSDK.Controllers
{
    public class OrderController : Controller
    {
        private MenuService _menuService;

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
        public ActionResult Menu()
        {
            MenuViewModel model = new MenuViewModel();

            foreach (var i in _menuService.GetShowMenu())
            {
                model.restaurants.Add(new MenuViewModel.Restaurant
                {
                    name = i.restaurant,
                    notice = i.notice,
                    menus = i.menus.Select(t => new Menu
                    {
                        name = t.name,
                        notice = t.notice,
                        price = t.price.Value,
                    }).ToList(),
                });
            }

            return View(model);
        }

    }
}