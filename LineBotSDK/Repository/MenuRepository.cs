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

        #region (+)  取得顯示餐廳
        /// <summary>
        /// (+)  取得顯示餐廳
        /// </summary>
        public List<Menu_M> GetShow()
        {
            return collection.Find(t => t.isShow == true).ToList();
        }
        #endregion

        public List<Menu_M> Get(string restaurant)
        {
            return collection.Find(t => t.restaurant == restaurant).ToList();
        }

        #region (+)  取得餐廳資訊
        /// <summary>
        /// (+)  取得餐廳資訊
        /// </summary>
        /// <param name="restaurant">餐廳名稱</param>
        /// <returns></returns>
        public Menu_M GetByName(string restaurant)
        {
            return collection.Find(t => t.restaurant == restaurant).FirstOrDefault();
        }
        #endregion

        //public List<Menu_M>


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

        #region (+)  新增餐廳
        /// <summary>
        /// (+)  新增餐廳
        /// </summary>
        /// <param name="data">餐廳資料</param>
        public void Add(Menu_M data)
        {
            collection.InsertOneAsync(data);
        }
        #endregion
    }
}