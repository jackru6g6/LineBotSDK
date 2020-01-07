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

        public List<Order> Orders;



        public class Order
        {
            public string Restaurant { get; set; }

            public DateTime OrderTime { get; set; }

            public string Notice { get; set; }

            public int Total { get; set; }

            public List<Order> Orders { get; set; }
        }

        public class _Orders
        {
            public string Name { get; set; }
            public string Notice { get; set; }
            public int Price { get; set; }
            public int Count { get; set; }
        }

    }
}