using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotSDK.Repository
{
    public class _BaseMongoRepository
    {
        protected IMongoClient _mongoClient;
        //private MongoServer _mongoServer;
        protected IMongoDatabase _mongoDatabase;
        //protected IMongoCollection<T> _mongoCollection<T>;

        public _BaseMongoRepository(string dataBase = "LineBot")
        {
            // MongoDB 連線字串
            string connectionString = "mongodb://localhost";
            // 產生 MongoClient 物件
            _mongoClient = new MongoClient(connectionString);

            // 取得 MongoServer 物件
            //_mongoServer = _mongoClient.GetServer();

            // 取得 MongoDatabase 物件
            // _mongoDatabase = _mongoClient.GetDatabase("LineBot");
            _mongoDatabase = _mongoClient.GetDatabase(dataBase);

            // 取得 Collection
            //_mongoCollection = _mongoDatabase.GetCollection<Member>("Member");
            //_mongoCollection = _mongoDatabase.GetCollection<Member>(collection);
        }

        #region (#)  取得 Collection 物件
        /// <summary>
        /// (#)  取得 Collection 物件
        /// </summary>
        /// <typeparam name="T"> 對應 MongoModel </typeparam>
        /// <param name="collection"> Collection 名稱</param>
        /// <returns></returns>
        protected IMongoCollection<T> GetMongoCollection<T>(string collection)
        {
            return _mongoDatabase.GetCollection<T>(collection);
        }
        #endregion
    }
}