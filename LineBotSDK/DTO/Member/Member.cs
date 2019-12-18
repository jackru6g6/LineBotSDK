using MongoDB.Bson.Serialization.Attributes;

namespace LineBotSDK.DTO.Member
{
    public class Member
    {
        [BsonId]
        public MongoDB.Bson.ObjectId _id { get; set; }

        public string UID { get; set; }

        public string Name { get; set; }
    }
}