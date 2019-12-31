using HtmlAgilityPack;
using LineBotSDK.Models.Mongodb;
using LineBotSDK.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotSDK.Service.Menu
{
    public class MenuService
    {
        private MenuRepository _repository = new MenuRepository();

        public MenuService() { }




        public Menu_M GetMenu(string restaurant)
        {
            var data = _repository.GetByName(restaurant);

            if (data == null)///沒有菜單
            {
                Add(WebCrawlerMenu(restaurant));
            }
            else if (data.updateDate < DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek)))///更新時間低於一周
            {
                Update(WebCrawlerMenu(restaurant));
            }

            return data;
        }

        private void Add(List<Menu_M._Menus> menus)
        {
            _repository.Add(new Menu_M
            {
                restaurant = "蜂鳥食堂",
                website = "https://www.hummingfood.com/menu",
                updateDate = DateTime.Now,
                menus = menus,
            });
        }

        private void Update(List<Menu_M._Menus> menus)
        {
            _repository.Update(new Menu_M
            {
                restaurant = "",
                updateDate = DateTime.Now,
                website = "",
                notice = string.Empty,
                menus = menus,
            });
        }


        /// <summary>
        /// (+)  爬網餐廳資料(菜單內容)
        /// </summary>
        /// <param name="restaurant">餐廳名稱</param>
        private List<Menu_M._Menus> WebCrawlerMenu(string restaurant)
        {
            HtmlWeb webClient = new HtmlWeb();
            HtmlDocument doc = webClient.Load("https://www.hummingfood.com/menu");

            //var aaa = doc.DocumentNode.SelectNodes("/html/body");

            //foreach (var i in aaa)
            //{
            //    var h3 = i.ChildNodes.SelectMany(x => x.Descendants("//h3")).ToList();
            //    var aFirst = i.ChildNodes[0].Descendants();
            //    var aaaaa = i.ChildNodes.SelectMany(x => x.Descendants("h3"));
            //    var aaaa = i.ChildNodes.SelectMany(x => x.Descendants("h3")).Select(x => x.InnerText).ToList();
            //}

            //var h3a = doc.DocumentNode.SelectNodes("//h3[contains(@class, 'dish-title')]"); ///取得幾樣菜

            //var dishsCount = doc.DocumentNode.SelectNodes("//div[contains(@class, 'HF-dish-box')]").Count;


            var menus = new List<Menu_M._Menus>();
            foreach (var dishs in doc.DocumentNode.SelectNodes("//div[contains(@class, 'HF-dish-box')]")) ///取得幾樣菜
            {
                HtmlDocument _doc = new HtmlDocument();
                _doc.LoadHtml(dishs.InnerHtml);


                //var aaaaaaa = dishs.SelectSingleNode("//span[contains(@class, 'dish-price')]");

                //var aa = dishs;
                //var ass = aa.OwnerDocument;
                //var name =  ass.DocumentNode.SelectNodes("//h3[contains(@class, 'dish-title')]");
                //var name = dishs.ChildNodes.SelectMany(t => t.Descendants("h3")).Select(t => t.InnerText).FirstOrDefault();


                var name = _doc.DocumentNode.SelectSingleNode("//h3[(@class='dish-title')]").InnerText;
                var price = _doc.DocumentNode.SelectSingleNode("//span[(@class='dish-price')]").InnerText;

                int _price = 0;
                if (int.TryParse(price, out _price))
                {

                }

                menus.Add(new Menu_M._Menus
                {
                    name = name,
                    price = _price,
                });
                //var remainCount = dishs.SelectSingleNode("//span[(@class='dish-price')]").InnerText;
                //var prices = dishs.ChildNodes.SelectMany(t => t.Descendants("span")).Select(t => t.InnerText).ToList();

                //var price1 = dishs.ChildNodes.SelectMany(t => t.Attributes["span"].Value).Select(t => t.InnerText).ToList();
            }
            return menus;


        }

    }
}