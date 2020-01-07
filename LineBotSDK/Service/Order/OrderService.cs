using isRock.LineBot;
using isRock.LineBot.Conversation;
using LineBotSDK.Models.Mongodb;
using LineBotSDK.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LineBotSDK.Models.LineConversation.Order_C;

namespace LineBotSDK.Service.Order
{
    public class OrderService
    {
        private OrderRepository _repository = new OrderRepository();
        private string Uid;

        public OrderService(string uid)
        {
            Uid = uid;
        }



        #region (+)  QuickReply_點餐
        /// <summary>
        /// (+)  QuickReply_點餐
        /// </summary>
        /// <param name="msg">訊息</param>
        public void QuickReply_Order(TextMessage textMsg)
        {
            switch (textMsg.text)
            {
                case "請問您要點哪一餐?":
                    textMsg.quickReply.items.Add(new QuickReplyMessageAction("早餐", "Breakfast"));
                    textMsg.quickReply.items.Add(new QuickReplyMessageAction("午餐", "Lunch"));
                    textMsg.quickReply.items.Add(new QuickReplyMessageAction("晚餐", "Dinner"));
                    break;
                case "請問您想吃的餐廳?":
                    textMsg.quickReply.items.Add(new QuickReplyMessageAction("皮可尼克", "皮可尼克"));
                    textMsg.quickReply.items.Add(new QuickReplyMessageAction("杜家", "杜家"));
                    textMsg.quickReply.items.Add(new QuickReplyMessageAction("uMeal", "優膳糧"));
                    break;
                case "請問您想點的餐點名稱?":
                    textMsg.quickReply.items.Add(new QuickReplyMessageAction("不知道~", "不知道~"));
                    break;
            }
            textMsg.quickReply.items.Add(new QuickReplyMessageAction("終止問答", "break"));
        }
        #endregion

        #region (+)  取得點餐對話物件
        /// <summary>
        /// (+)  取得點餐對話物件
        /// </summary>
        /// <typeparam name="Order_Request"></typeparam>
        /// <param name="channelAccessToken">line bot token</param>
        /// <returns></returns>
        public InformationCollector<Order_Request> GetConversationCIC(string channelAccessToken)
        {
            //定義資訊蒐集者
            isRock.LineBot.Conversation.InformationCollector<Order_Request> CIC = new isRock.LineBot.Conversation.InformationCollector<Order_Request>(channelAccessToken);

            #region 規則
            CIC.OnMessageTypeCheck += (s, e) =>
                {
                    var verifications = new List<string>();

                    switch (e.CurrentPropertyName)
                    {
                        case "type":
                            verifications = new List<string> { "Breakfast", "Lunch", "Dinner" };
                            break;
                        case "restaurant":
                            verifications = new List<string> { "皮可尼克", "杜家", "優膳糧" };
                            break;
                        case "meal":
                            verifications = new List<string> { "不知道~" };
                            break;
                    }

                    if (!verifications.Any(t => t == e.ReceievedMessage))
                    {
                        e.isMismatch = true;
                        e.ResponseMessage = "回應錯誤~\r\n請點擊橢圓按鈕進行程序";
                    }

                };
            #endregion

            return CIC;
        }
        #endregion




        #region (+)  取得該天訂單
        /// <summary>
        /// (+)  取得該天訂單
        /// </summary>
        /// <param name="date">日期</param>
        public List<Order_M> GetAllOrderByDate(string date)
        {
            DateTime _date;

            if (DateTime.TryParse(date, out _date))
            {
                return _repository.SelectAllByDate(Uid, _date).ToList();
            }
            return null;
        }
        public List<Order_M> GetAllOrderByDate(DateTime date)
        {
            return _repository.SelectAllByDate(Uid, date).ToList();
        }
        #endregion

        #region (+)  新增訂單
        /// <summary>
        /// (+)  新增訂單
        /// </summary>
        /// <param name="jason">物件的 jason 字串</param>
        public void AddOrder(string jason)
        {
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Order_M>(jason);
            data.uid = Uid;
            data.updateDate = DateTime.Now;

            _repository.Add(data);
        }
        #endregion

        #region (+)  刪除訂單
        /// <summary>
        /// (+)  刪除訂單
        /// </summary>
        public void DeleteOrder(DeleteOrderDto data)
        {
            _repository.Delete(Uid, data);
        }
        #endregion

        #region (~)  刪除訂單物件
        /// <summary>
        /// (~)  刪除訂單物件
        /// </summary>
        public class DeleteOrderDto
        {
            //public string type { get; set; }
            public string restaurant { get; set; }
            public DateTime date { get; set; }
        }
        #endregion
    }
}