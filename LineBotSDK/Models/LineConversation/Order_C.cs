using isRock.LineBot.Conversation;

namespace LineBotSDK.Models.LineConversation
{
    public class Order_C
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
    }
}