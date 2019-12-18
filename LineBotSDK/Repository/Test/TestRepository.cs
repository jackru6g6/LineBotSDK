using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LineBotSDK.Repository.Test
{
    public class TestRepository
    {
        


        public class TTT
        {

            [BsonId]
            public MongoDB.Bson.ObjectId _id { get; set; }

            //[BsonRepresentation(BsonType.ObjectId)]
            //public string ID { get; set; }

            public DateTime CreateDateTime { get; set; }
            public string IpAddress { get; set; }
            public string SourceUserID { get; set; }
            public string ActionData { get; set; }
            public string SourceJsonString { get; set; }
            public string ReturnJsonString { get; set; }
            public string Details { get; set; }
            public string MagData { get; set; }
            public string Sort { get; set; }
        }

        public class TT        {

            [BsonId]
            public MongoDB.Bson.ObjectId _id { get; set; }

            //[BsonRepresentation(BsonType.ObjectId)]
            //public string ID { get; set; }

            public DateTime CreateDateTime { get; set; }
            public string IpAddress { get; set; }
            public string SourceUserID { get; set; }
            public string ActionData { get; set; }
            public string SourceJsonString { get; set; }
            public string ReturnJsonString { get; set; }
            public string Details { get; set; }
            public string MagData { get; set; }
        }

        public TestRepository()
        {

            Select();
            IMongoClient _client = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase _database = _client.GetDatabase("LineBot");//設定條件(更多條件設定) using MongoDB.Driver.Builders;
            var ErrorLog = _database.GetCollection<TT>("ChatTest");


            //IMongoCollection<TTT> a = new MongoClient("mongodb://localhost:27017").GetDatabase("LineBot").GetCollection<TTT>("ChatTest");


            //BsonDocument ErrorData = new BsonDocument{
            //    {"CreateDateTime", DateTime.Now},
            //    {"IpAddress", HttpContext.Current.Request.UserHostAddress},
            //    {"SourceUserID", ""},
            //    {"ActionData", "tActionData"},
            //    {"SourceJsonString", "tSourceJsonString"},
            //    {"ReturnJsonString", ""},
            //    {"Details" ,"tDetails"},
            //    {"MagData", "tMagData"},
            //    {"Sort", "1"}
            //};

            TT ErrorData = new TT
            {
                CreateDateTime = DateTime.Now,
                IpAddress = "127.7.7.0"
            };


            ErrorLog.InsertOneAsync(ErrorData);



        }

        private void Select()
        {
            // mongodb 連線字串
            var connString = "mongodb://127.0.0.1:27017";
            //建立 mongo client
            var client = new MongoClient(connString);
            //取得 database
            var db = client.GetDatabase("LineBot");
            //取得 user collection
            var collection = db.GetCollection<TT>("ChatTest");

            //依 name 過濾並取得一筆資料
            var document = collection.Find(t => t.IpAddress == "127.7.7.0").FirstOrDefault();

            var a = collection.AsQueryable().ToList();

        }
    }
}