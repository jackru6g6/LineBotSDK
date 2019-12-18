using isRock.LineBot;
using LineBotSDK.Repository;
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
        static int _Index = 0;

        private Bot _Bot;
        private string _ChannelAccessToken;

        private ReceievedMessage _SendData;
        private List<Event> _Event;

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
            try
            {
                ReceivedMessageRequest();

                if (_Event[0].message.text.Contains("加入會員") == true)
                {
                    MemberRepository _member = new MemberRepository();
                    if (!_member.IsMember(_Event[0].source.userId))
                    {
                        _Index++;
                        _member.Add(_Event[0].source.userId, _Index.ToString());
                        ReplyMessage("歡迎加入會員~");
                    }
                    else
                    {
                        ReplyMessage("您已經是會員了！");
                    }
                }

                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
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
            isRock.LineBot.Utility.ReplyMessage(_Event[0].replyToken, message, _ChannelAccessToken);
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
            _Event = _SendData.events;
        } 
        #endregion



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