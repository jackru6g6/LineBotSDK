using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotSDK.Models.Mongodb
{
    public class Order_M : _Mongodb
    {
        public Order_M() { }

        public string uid { get; set; }
        public string type { get; set; }
        public string restaurant { get; set; }
        public string meal { get; set; }
        public DateTime orderTime { get; set; }
    }
}