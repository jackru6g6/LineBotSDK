using isRock.LineBot.Conversation;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LineBotSDK.DTO.Member
{
    public class Member_M : Mongodb
    {
        public Member_M()
        {
            status = new Status_M();
        }

        public string UID { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public bool isManagement { get; set; }

        public Status_M status { get; set; }
    }

    public class Status_M : Mongodb
    {
        public Status_M() { }

        public string text { get; set; }
    }


}