using isRock.LineBot;
using isRock.LineBot.Conversation;
using LineBotSDK.Repository;
using LineBotSDK.Service.Member;
using LineBotSDK.Service.Menu;
using LineBotSDK.Service.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LineBotSDK.Controllers
{
    public class LineChatController : ApiController //: isRock.LineBot.LineWebHookControllerBase
    {
        private Bot _Bot;
        private string _ChannelAccessToken;
        private ReceievedMessage _SendData;
        private List<Event> _LineEvents;
        private Event _LineEvent;

        /// <summary> (-) 目前使用者對話狀態 </summary>
        private string _Statue;

        /// <summary> (-) 回應訊息( TextMessage ) </summary>
        private string _ReplyMessage;
        isRock.LineBot.TextMessage _TextMessage = new TextMessage(string.Empty); ///內容不可為空，label不可超過20字

        private MemberService _MemberService;

        public LineChatController()
        {
            ///設定你的Channel Access Token
            _ChannelAccessToken = System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("LineChannelAccessToken") ? System.Configuration.ConfigurationManager.AppSettings["LineChannelAccessToken"] :
                "+Y4SrvfIBUhNJQDXVyyEJtDzOrNjllgX4jxkzmlHDJGQvSv5EZ5AvVifogzqMpyczzkDZP1wcsORVo8Cejgz8mffCePvRp9qtsYChnvK0AVB+hRIaSI0zmuNKGuy8hUYviasjE09AgUbaJ+FbvT8xQdB04t89/1O/w1cDnyilFU=";

            ///設定機器人物件
            _Bot = new isRock.LineBot.Bot(_ChannelAccessToken);
        }

        //[Route("api/LineWebHookSample")] ///https://f54d8f8b.ngrok.io/LineBotSDK/api/LineWebHookSample
        [HttpPost]
        public IHttpActionResult POST()
        {
            try
            {
                ReceivedMessageRequest();
                _MemberService = new MemberService(_LineEvent.source.userId);

                ///第一次加LineBot好友或解除封鎖，自動加入會員
                if (_LineEvent.type == "follow")
                {
                    FollewBot();
                    return Ok();
                }
                else if (_LineEvent.type == "unfollow")
                {
                    ///封鎖機器人
                    return Ok();
                }
                else
                {
                    _Statue = _MemberService.GetStatue();

                    ///第一次使用功能，不會有狀態碼，所以給予使用者動作狀態
                    _Statue = string.IsNullOrWhiteSpace(_Statue) ? _LineEvent.message.text : _Statue;
                }



                switch (_Statue)
                {
                    case "SelectOrder":
                        ///查詢訂單
                        SelectOrder();
                        break;
                    case "OrderMeal":
                        ///點單
                        OrderMeal();
                        break;
                    case "DeleteOrder":
                        ///刪除訂單
                        DeleteOrder();
                        break;
                    case "WebOpen":

                        //Models.Order.OrderModel a = new Models.Order.OrderModel();
                        //a.Test();

                        var LiffURL = "https://f75f8205.ngrok.io/LineBotSDK/home/index";
                        //建立LiffApp
                        var Liff = isRock.LIFF.Utility.AddLiffApp(_ChannelAccessToken, new Uri(LiffURL), isRock.LIFF.ViewType.compact);
                        //顯示建立好的 Liff App
                        isRock.LineBot.Utility.PushMessage(_LineEvent.source.userId, Liff, _ChannelAccessToken);
                        //isRock.LIFF.Utility.DeleteLiffApp(_ChannelAccessToken, Liffid.apps[0].liffId);
                        break;
                    case "WebCrawlerMenu":
                        //MenuService _menuService = new MenuService();
                        //_menuService.WebCrawlerMenu("");
                        break;
                    default:
                        return Ok();
                }




                if (!string.IsNullOrWhiteSpace(_ReplyMessage))
                {
                    ReplyMessage(_ReplyMessage);
                }

                if (_TextMessage?.quickReply.items.Any() == true)
                {
                    _Bot.PushMessage(_LineEvent.source.userId, _TextMessage);
                }


                _MemberService.SetStatus(_Statue);

                //回覆API OK
                return Ok();
            }
            catch (Exception)
            {
                //ReplyMessage("伺服器有誤!~\r\n請稍後再嘗試");
                return Ok();
            }
        }

        #region (-)  回應訊息
        /// <summary>
        /// (-)  回應訊息
        /// </summary>
        /// <param name="message">訊息內容</param>
        private void ReplyMessage(string message)
        {
            //回覆用戶
            isRock.LineBot.Utility.ReplyMessage(_LineEvent.replyToken, message, _ChannelAccessToken);
        }
        #endregion

        #region (-)  解析來源資料並賦值
        /// <summary>
        /// (-)  解析來源資料並賦值
        /// </summary>
        private void ReceivedMessageRequest()
        {
            //取得 http Post RawData(should be JSON)
            string postData = Request.Content.ReadAsStringAsync().Result;
            //剖析JSON
            _SendData = isRock.LineBot.Utility.Parsing(postData);
            _LineEvents = _SendData.events;
            _LineEvent = _LineEvents.FirstOrDefault();
        }
        #endregion



        #region (-)  跟隨機器人動作
        /// <summary>
        /// (-)  跟隨機器人動作
        /// </summary>
        private void FollewBot()
        {
            ///解除機器人或第一次增加好友
            if (_MemberService.isMember())
            {
                ReplyMessage("歡迎回來~");
            }
            else
            {
                ///取得用戶資料(Uid、暱稱、照片Url)
                var userInfo = _Bot.GetUserInfo(_LineEvent.source.userId);
                _MemberService.JoinMember(userInfo.displayName, userInfo.pictureUrl);
                ReplyMessage("歡迎加入~");
            }
        }
        #endregion

        #region (-)  查詢訂單動作
        /// <summary>
        /// (-)  查詢訂單動作
        /// </summary>
        /// <param name="service"></param>
        /// <param name="textMsg"></param>
        private void SelectOrder()
        {
            _Statue = "SelectOrder";

            if (_LineEvent.type == "message")
            {
                ///查詢訂單動作
                _TextMessage = new TextMessage("請問您要查詢哪天訂單?");
                _TextMessage.quickReply.items.Add(new QuickReplyDatetimePickerAction("選取查詢日期", "data", DatetimePickerModes.date));
            }
            else if (_LineEvent.type == "postback")
            {
                ///選擇日期完動作
                OrderService service = new OrderService(_LineEvent.source.userId);
                var orders = service.GetAllOrderByDate(_LineEvent.postback.Params.date);///格式 yyyy-MM-dd

                _ReplyMessage = orders?.Any() == true ? $"{string.Join("\r\n\r\n", orders.Select(t => $" 哪一餐：{t.type}\r\n 餐廳：{t.restaurant}\r\n 餐點內容：{t.meal}\r\n 訂購時間：{t.orderTime}"))}"
                                                                               : $"{_LineEvent.postback.Params.date} 查無點餐紀錄";
                _Statue = string.Empty;
            }
        }
        #endregion

        #region (-)  點菜單動作
        /// <summary>
        /// (-)  點菜單動作
        /// </summary>
        private void OrderMeal()
        {
            _Statue = "OrderMeal";

            //定義接收CIC結果的類別
            ProcessResult<Models.LineConversation.Order_C.Order_Request> result;
            OrderService service = new OrderService(_LineEvent.source.userId);

            var CIC = service.GetConversationCIC(_ChannelAccessToken);
            if (_LineEvent.message.text == "OrderMeal")
            {
                ///開始點餐程序

                //把訊息丟給CIC 
                result = CIC.Process(_SendData.events.FirstOrDefault(), true);
            }
            else
            {
                //把訊息丟給CIC 
                result = CIC.Process(_LineEvent);
            }

            //isRock.LineBot.TextMessage textMsg = new isRock.LineBot.TextMessage(result.ResponseMessageCandidate);

            _TextMessage = new isRock.LineBot.TextMessage(result.ResponseMessageCandidate); ;

            //處理 CIC回覆的結果
            switch (result.ProcessResultStatus)
            {
                case ProcessResultStatus.Processed:
                    ///繼續對話

                    service.QuickReply_Order(_TextMessage);
                    //_Bot.PushMessage(_LineEvent.source.userId, _TextMessage);
                    return;
                case ProcessResultStatus.Done:
                    ///完成對話
                    service.AddOrder(Newtonsoft.Json.JsonConvert.SerializeObject(result.ConversationState.ConversationEntity));

                    _ReplyMessage = "完成訂單！！";
                    _Statue = string.Empty;
                    return;
                case ProcessResultStatus.Pass:
                    ///目前不再對話狀態中，非典餐過程
                    _Statue = string.Empty;
                    break;
                case ProcessResultStatus.Exception:
                    ///取得候選訊息發送，例外
                    _ReplyMessage += result.ResponseMessageCandidate;
                    break;
                case ProcessResultStatus.Break:
                    ///離開對話
                    ///responseMsg += result.ResponseMessageCandidate;
                    _ReplyMessage = "中止點餐";
                    _Statue = string.Empty;
                    return;
                case ProcessResultStatus.InputDataFitError:
                    ///內容轉型失敗

                    ///可以判斷狀態再重新計一次QuickReply
                    service.QuickReply_Order(_TextMessage);
                    return;
                default:
                    //取得候選訊息發送
                    _ReplyMessage += result.ResponseMessageCandidate;
                    break;
            }
        }
        #endregion

        #region (-)  刪除訂單動作
        /// <summary>
        /// (-)  刪除訂單動作
        /// </summary>
        /// <param name="service"></param>
        private void DeleteOrder()
        {
            _Statue = "DeleteOrder";

            if (_LineEvent.type == "message")
            {
                if (_LineEvent.message.text.Split('/')?.Count() > 1)
                {
                    ///選取要刪除訂單內容

                    var datas = _LineEvent.message.text.Split('/');

                    OrderService service = new OrderService(_LineEvent.source.userId);
                    service.DeleteOrder(new OrderService.DeleteOrderDto
                    {
                        type = datas[0],
                        restaurant = datas[1],
                        meal = datas[2],
                    });

                    _ReplyMessage = "刪除訂單完成";
                    _Statue = string.Empty;
                }
                else
                {
                    ///刪除訂單動作
                    _TextMessage.text = "請問您要刪除哪天訂單?";
                    _TextMessage.quickReply.items.Add(new QuickReplyDatetimePickerAction("選取刪除日期", "data", DatetimePickerModes.date));
                }
            }
            else if (_LineEvent.type == "postback")
            {
                ///選擇日期完動作
                OrderService service = new OrderService(_LineEvent.source.userId);

                var orders = service.GetAllOrderByDate(_LineEvent.postback.Params.date);///格式 yyyy-MM-dd

                if (orders?.Any() == false)
                {
                    _ReplyMessage = $"{_LineEvent.postback.Params.date} 並無餐紀錄";
                    return;
                }

                _TextMessage.text = "請選擇要刪除的訂單?";
                foreach (var i in orders)
                {
                    _TextMessage.quickReply.items.Add(new QuickReplyMessageAction($"{i.type}/{i.restaurant}/{i.meal}", $"{i.type}/{i.restaurant}/{i.meal}"));
                }
            }
        }
        #endregion



    }
}