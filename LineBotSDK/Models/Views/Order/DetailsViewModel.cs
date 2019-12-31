using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotSDK.Models.Views.Order
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
            Orders = new List<Order>();
        }

        public string RestaurantName { get; set; }

        public DateTime ChooseDate { get; set; }

        public List<Order> Orders { get; set; }

        public class Order
        {
            public string DishName { get; set; }
            public string Notice { get; set; }
            public int Price { get; set; }
        }
    }
}