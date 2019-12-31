namespace LineBotSDK.Models.Mongodb
{
    public class Member_M : _Mongodb
    {
        public Member_M()
        {
            status = new _Status();
        }

        public string UID { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public bool isManagement { get; set; }

        public _Status status { get; set; }

        public class _Status
        {
            public _Status() { }

            public string text { get; set; }
        }
    }
}