using isRock.LineBot.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotSDK.DTO.Order
{
    public class Order
    {
        public class Order_Request : ConversationEntity
        {
            [Question("請問您要點哪一餐?")]
            [Order(1)]
            public string type { get; set; }

            [Question("請問您想吃的餐廳?")]
            [Order(2)]
            public string restaurant { get; set; }

            [Question("請問您想點的餐點名稱?")]
            [Order(3)]
            public string meal { get; set; }
        }

        public class Order_M : Mongodb
        {
            public string uid { get; set; }
            public string type { get; set; }
            public string restaurant { get; set; }
            public string meal { get; set; }
            public DateTime orderTime { get; set; }
        }
    }
}