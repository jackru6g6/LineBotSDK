using MongoDB.Bson.Serialization.Attributes;

namespace LineBotSDK.Models.Mongodb
{
    public class _Mongodb
    {
        public _Mongodb() { }

        [BsonId]
        public MongoDB.Bson.ObjectId _id { get; set; }
    }
}