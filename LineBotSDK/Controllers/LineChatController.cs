using LineBotSDK.Repository.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LineBotSDK.Controllers
{
    public class LineChatController : ApiController
    {
        isRock.LineBot.Bot _Bot;
        string _ChannelAccessToken;
        public LineChatController()
        {
            ///設定你的Channel Access Token
            _ChannelAccessToken = System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("LineChannelAccessToken") ? System.Configuration.ConfigurationManager.AppSettings["LineChannelAccessToken"] :
                "+Y4SrvfIBUhNJQDXVyyEJtDzOrNjllgX4jxkzmlHDJGQvSv5EZ5AvVifogzqMpyczzkDZP1wcsORVo8Cejgz8mffCePvRp9qtsYChnvK0AVB+hRIaSI0zmuNKGuy8hUYviasjE09AgUbaJ+FbvT8xQdB04t89/1O/w1cDnyilFU=";

            ///設定機器人物件
            _Bot = new isRock.LineBot.Bot(_ChannelAccessToken);
        }

        [HttpPost]
        public IHttpActionResult POST()
        {
            TestRepository test = new TestRepository();
            /*
            try
            {
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                var UserSays = ReceivedMessage.events[0].message.text;
                var ReplyToken = "Udaa293df6f3c802cbc2f8ca03c93ceb6";//ReceivedMessage.events[0].replyToken;
                //依照用戶說的特定關鍵字來回應
                switch (UserSays.ToLower())
                {
                    case "/teststicker":
                        //回覆貼圖
                        bot.ReplyMessage(ReplyToken, 1, 1);
                        break;
                    case "/testimage":
                        //回覆圖片
                        bot.ReplyMessage(ReplyToken, new Uri("https://scontent-tpe1-1.xx.fbcdn.net/v/t31.0-8/15800635_1324407647598805_917901174271992826_o.jpg?oh=2fe14b080454b33be59cdfea8245406d&oe=591D5C94"));
                        break;
                    default:
                        //回覆訊息
                        string Message="哈囉, 你說了:" + UserSays;
                        //回覆用戶
                        bot.ReplyMessage(ReplyToken, Message);
                        break;
                }
                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
            */

            try
            {
                ////傳送純文字訊息
                //bot.PushMessage("Udaa293df6f3c802cbc2f8ca03c93ceb6", UserSays);

                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);

                //回覆訊息
                string message = ReceivedMessage.events[0].message.text;
                if (message.Contains("Barry"))
                {
                    TalkToBarry(message);
                }
                else
                {
                    TalkTo(ReceivedMessage.events[0].replyToken, message);
                }

                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }


        private void TalkToBarry(string mseeage)
        {
            mseeage = mseeage.Replace("Barry", string.Empty).Trim();
            ///回覆Barry
            _Bot.PushMessage("Udaa293df6f3c802cbc2f8ca03c93ceb6", $"川普娃娃說：我是爛禮物~\r\n{mseeage}");//U635b9e9c403a641d4cf3b10e894427ee
        }

        private void TalkTo(string replyToken, string message)
        {
            //回覆用戶
            isRock.LineBot.Utility.ReplyMessage(replyToken, $"回復機器人：{message}", _ChannelAccessToken);
        }

    }
}