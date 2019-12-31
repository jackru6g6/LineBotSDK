using LineBotSDK.Models.Mongodb;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;

namespace LineBotSDK.Repository
{
    public class MemberRepository : _BaseMongoRepository
    {
        //private IMongoClient _mongoClient;
        ////private MongoServer _mongoServer;
        //private IMongoDatabase _mongoDatabase;
        //private IMongoCollection<Member> _mongoCollection;

        private IMongoCollection<Member_M> _mongoCollection;

        public MemberRepository() : base()
        {
            //// MongoDB 連線字串
            //string connectionString = "mongodb://localhost";
            //// 產生 MongoClient 物件
            //_mongoClient = new MongoClient(connectionString);

            //// 取得 MongoServer 物件
            ////_mongoServer = _mongoClient.GetServer();

            //// 取得 MongoDatabase 物件
            //_mongoDatabase = _mongoClient.GetDatabase("LineBot");
            //// 取得 Collection
            //_mongoCollection = _mongoDatabase.GetCollection<Member>("Member");

            _mongoCollection = GetMongoCollection<Member_M>("Member");
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
            _mongoCollection.InsertOneAsync(new Member_M
            {
                UID = UID,
                Name = name,
                PictureUrl = picUrl,
            });
        }
        #endregion

        #region (+)  更新使用者目前使用狀態
        /// <summary>
        /// (+)  更新使用者目前使用狀態
        /// </summary>
        /// <param name="userUid">使用者 uid</param>
        /// <param name="status">狀態</param>
        public void UpdateStatusByUid(string userUid, string status)
        {
            //var query = Query<Member>.EQ(t => t.UID, "");

            //var set = Update<Member>.Set(p => p.status.text, "status");
            //_mongoCollection.UpdateOne(query, set);


            var query = Builders<Member_M>.Filter.Eq(t => t.UID, userUid);
            var set = Builders<Member_M>.Update.Set(p => p.status.text, status);
            _mongoCollection.UpdateOne(query, set);
        }
        #endregion

        #region (+)  取得使用者狀態
        /// <summary>
        /// (+)  取得使用者狀態
        /// </summary>
        /// <param name="userUid">使用者 uid</param>
        public string SelectStatusByUid(string userUid)
        {
            return _mongoCollection.Find(t => t.UID == userUid).FirstOrDefault()?.status.text;
        } 
        #endregion

    }
}