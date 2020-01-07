using LineBotSDK.Models.Mongodb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using static LineBotSDK.Service.Order.OrderService;

namespace LineBotSDK.Repository
{
    public class OrderRepository : _BaseMongoRepository
    {
        private IMongoCollection<Order_M> collection;

        public OrderRepository() : base()
        {
            collection = GetMongoCollection<Order_M>("Order");
        }

        #region  (+)  取得使用者全部訂單
        /// <summary>
        /// (+)  取得使用者全部訂單
        /// </summary>
        /// <param name="uid">使用者Uid</param>
        /// <returns></returns>
        public List<Order_M> SelectAllByDate(string uid, DateTime date)
        {
            return collection.Find(t => t.uid == uid &&
                                                     t.updateDate >= date &&
                                                     t.updateDate < date.AddDays(1)).ToList();
        }
        #endregion

        #region (+)  新增訂單
        /// <summary>
        /// (+)  新增訂單
        /// </summary>
        public void Add(Order_M data)
        {
            collection.InsertOneAsync(data);
        }
        #endregion

        #region (+)  刪除訂單
        /// <summary>
        /// (+)  刪除訂單
        /// </summary>
        /// <param name="uid">使用者uid</param>
        /// <param name="data">訂購時間</param>
        public void Delete(string uid, DeleteOrderDto data)
        {
            //var query = Builders<Member_M>.Filter.Eq(t => t.UID, userUid);
            //var set = Builders<Member_M>.Update.Set(p => p.status.text, status);
            collection.DeleteOne(t => t.uid == uid &&
                                                    //t.type == data.type &&
                                                    t.restaurant == data.restaurant &&

                                                    t.updateDate >= data.date &&
                                                     t.updateDate < data.date.AddDays(1)
                                                    //t.meal == data.meal
                                                    );


            //collection.DeleteOne(a => a.uid == uid);

            //collection.DeleteOne(a => a.uid == uid &&
            //                                        a.orderTime.ToString("yyyyMMdd") == orderTime.ToString("yyyyMMdd"));


            //var query = Query<Order_M>.EQ(p => p.uid, "");
            //collection.Remove(query, RemoveFlags.Single, WriteConcern.Unacknowledged);
        }
        #endregion
    }
}