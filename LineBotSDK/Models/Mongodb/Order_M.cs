using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotSDK.Models.Mongodb
{
    public class Order_M : _Mongodb
    {
        public Order_M()
        {
            orders = new List<_order>();
        }

        public string uid { get; set; }
        public string restaurant { get; set; }
        public DateTime updateDate { get; set; }
        public string notice { get; set; }
        public decimal total { get; set; }
        public List<_order> orders { get; set; }

        public class _order
        {
            public string name { get; set; }
            public decimal price { get; set; }
            public string notice { get; set; }
            public int count { get; set; }
        }
    }
}