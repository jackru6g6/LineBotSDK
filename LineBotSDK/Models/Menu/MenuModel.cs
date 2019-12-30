using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotSDK.Models.Menu
{
    public class MenuModel
    {
        /// <summary> 菜名 </summary>
        public string Name { get; set; }

        /// <summary> 價錢 </summary>
        public int? Price { get; set; }

        /// <summary> 註記 </summary>
        public string Notice { get; set; }



    }
}