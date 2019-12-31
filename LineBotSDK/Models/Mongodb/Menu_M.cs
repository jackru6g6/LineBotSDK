using System;
using System.Collections.Generic;

namespace LineBotSDK.Models.Mongodb
{
    public class Menu_M : _Mongodb
    {
        public Menu_M()
        {
            menus = new List<_Menus>();
        }

        public string restaurant { get; set; }
        public string website { get; set; }
        public DateTime updateDate { get; set; }
        public string notice { get; set; }

        public List<_Menus> menus { get; set; }

        public class _Menus
        {
            public _Menus() { }
            public string name { get; set; }
            public int? price { get; set; }
            public string notice { get; set; }
        }
    }
}