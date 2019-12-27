using LineBotSDK.DTO.Member;
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

        /// <summary> 使用者 Line Uid </summary>
        private string UserUid;

        public MemberService(string userUid)
        {
            UserUid = userUid;
        }

        #region (+)  判斷是否為會員
        /// <summary>
        /// (+)  判斷是否為會員
        /// </summary>
        public bool isMember()
        {
            return _repository.IsMember(UserUid);
        }
        #endregion

        #region (+)  加入會員
        /// <summary>
        /// (+)  加入會員
        /// </summary>
        /// <param name="name">暱稱</param>
        /// <param name="picUrl"> Line 使用者照片 url </param>
        /// <returns></returns>
        public void JoinMember(string name, string picUrl)
        {
            _repository.Add(UserUid, name, picUrl);
        }
        #endregion


        #region (+)  取得會員狀態
        /// <summary>
        /// (+)  取得會員狀態
        /// </summary>
        /// <returns></returns>
        public string GetStatue()
        {
            return _repository.SelectStatusByUid(UserUid);
        }
        #endregion

        #region (+)  設定會員狀態
        /// <summary>
        /// (+)  設定會員狀態
        /// </summary>
        /// <param name="status">狀態</param>
        public void SetStatus(string status)
        {
            _repository.UpdateStatusByUid(UserUid, status);
        } 
        #endregion
    }
}