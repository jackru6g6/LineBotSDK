using LineBotSDK.DTO.Member;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotSDK.Repository
{
    public class MemberRepository
    {
        private IMongoClient _mongoClient;
        //private MongoServer _mongoServer;
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<Member> _mongoCollection;

        public MemberRepository()
        {
            // MongoDB 連線字串
            string connectionString = "mongodb://localhost";
            // 產生 MongoClient 物件
            _mongoClient = new MongoClient(connectionString);

            // 取得 MongoServer 物件
            //_mongoServer = _mongoClient.GetServer();

            // 取得 MongoDatabase 物件
            _mongoDatabase = _mongoClient.GetDatabase("LineBot");
            // 取得 Collection
            _mongoCollection = _mongoDatabase.GetCollection<Member>("Member");
        }

        public IMongoQueryable GetAll()
        {
            return _mongoCollection.AsQueryable();
        }

        public void GetByUID(string UID)
        {
            _mongoCollection.Find(t => t.UID == UID).FirstOrDefault();
        }

        #region (+)  判斷是不是會員
        /// <summary>
        /// (+)  判斷是不是會員
        /// </summary>
        /// <param name="UID">Line UID</param>
        /// <returns>true/false</returns>
        public bool IsMember(string UID)
        {
            return _mongoCollection.Find(t => t.UID == UID).FirstOrDefault() != null;
        }
        #endregion

        #region (+)  加入會員
        /// <summary>
        /// (+)  加入會員
        /// </summary>
        /// <param name="UID">Line UID</param>
        /// <param name="name">名稱/暱稱</param>
        /// <param name="picUrl">照片Url</param>
        public void Add(string UID, string name, string picUrl)
        {
            _mongoCollection.InsertOneAsync(new Member
            {
                UID = UID,
                Name = name,
                PictureUrl = picUrl,
            });
        }
        #endregion

    }
}