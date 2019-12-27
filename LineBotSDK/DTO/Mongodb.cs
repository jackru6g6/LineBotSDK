using MongoDB.Bson.Serialization.Attributes;

namespace LineBotSDK.DTO
{
    public class Mongodb
    {
        public Mongodb() { }

        [BsonId]
        public MongoDB.Bson.ObjectId _id { get; set; }
    }
}