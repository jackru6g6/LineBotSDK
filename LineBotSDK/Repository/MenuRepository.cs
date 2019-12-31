using LineBotSDK.Models.Mongodb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace LineBotSDK.Repository
{
    public class MenuRepository : _BaseMongoRepository
    {
        private IMongoCollection<Menu_M> collection;

        public MenuRepository()
        {
            collection = GetMongoCollection<Menu_M>("Restaurant");
        }


        public List<Menu_M> Get(string restaurant)
        {
            return collection.Find(t => t.restaurant == restaurant).ToList();
        }


        public Menu_M GetByName(string restaurant)
        {
            return collection.Find(t => t.restaurant == restaurant).FirstOrDefault();
        }


        #region (+)  更新菜單
        /// <summary>
        /// (+)  更新菜單
        /// </summary>
        /// <param name="data">菜單資料</param>
        public void Update(Menu_M data)
        {
            var query = Builders<Menu_M>.Filter.Eq(t => t.restaurant, data.restaurant);
            var set = Builders<Menu_M>.Update.Set(t => t.website, data.website)
                                                                    .Set(t => t.updateDate, DateTime.Now)
                                                                    .Set(t => t.menus, data.menus);
            collection.UpdateOne(query, set);
        }
        #endregion

        public void Add(Menu_M data)
        {
            collection.InsertOneAsync(data);
        }
    }
}