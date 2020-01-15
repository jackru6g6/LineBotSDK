using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotSDK.Models.Views.Order
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            restaurants = new List<Restaurant>();
        }

        public List<Restaurant> restaurants { get; set; }

        public class Restaurant
        {
            public Restaurant()
            {
                menus = new List<Menu>();
            }

            public string name { get; set; }
            public string notice { get; set; }
            public List<Menu> menus { get; set; }

            public class Menu
            {
                public string name { get; set; }
                public int price { get; set; }
                public string notice { get; set; }
            }
        }
    }
}