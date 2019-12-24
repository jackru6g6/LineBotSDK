using LineBotSDK.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotSDK.Service.Member
{
    public class MemberService
    {
        private MemberRepository _repository = new MemberRepository();


        #region (+)  加入會員
        /// <summary>
        /// (+)  加入會員
        /// </summary>
        /// <param name="userId">Line使用者UID</param>
        /// <param name="name">暱稱</param>
        /// <returns></returns>
        public string JoinMember(string userId, string name, string picUrl)
        {
            if (!_repository.IsMember(userId))
            {
                _repository.Add(userId, name, picUrl);
                return "歡迎加入會員~";
            }
            else
            {
                return "您已經是會員了！";
            }
        }
        #endregion



    }
}