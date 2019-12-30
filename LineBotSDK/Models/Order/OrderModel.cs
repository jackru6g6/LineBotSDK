using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotSDK.Models.Order
{
    public class OrderModel
    {



        public void Test()
        {
            HtmlWeb webClient = new HtmlWeb();
            HtmlDocument doc = webClient.Load("https://www.hummingfood.com/menu");

            var aaa = doc.DocumentNode.SelectNodes("/html/body");

            foreach (var i in aaa)
            {
                var h3 = i.ChildNodes.SelectMany(x => x.Descendants("//h3")).ToList();
                var aFirst = i.ChildNodes[0].Descendants();
                var aaaaa = i.ChildNodes.SelectMany(x => x.Descendants("h3"));
                var aaaa = i.ChildNodes.SelectMany(x => x.Descendants("h3")).Select(x => x.InnerText).ToList();
            }

            var h3a = doc.DocumentNode.SelectNodes("//h3[contains(@class, 'dish-title')]"); ///取得幾樣菜

            var dishsCount = doc.DocumentNode.SelectNodes("//div[contains(@class, 'HF-dish-box')]").Count;

            foreach (var dishs in doc.DocumentNode.SelectNodes("//div[contains(@class, 'HF-dish-box')]")) ///取得幾樣菜
            {
                var aaaaaaa = dishs.SelectSingleNode("//span[contains(@class, 'dish-price')]");

                var aa = dishs;
                var ass = aa.OwnerDocument;
                //var name =  ass.DocumentNode.SelectNodes("//h3[contains(@class, 'dish-title')]");
                //var name = dishs.ChildNodes.SelectMany(t => t.Descendants("h3")).Select(t => t.InnerText).FirstOrDefault();
                var name = dishs.SelectSingleNode("//h3[(@class='dish-title')]").InnerText;
                var price = dishs.SelectSingleNode("//span[(@class='dish-price')]").InnerText;
                var remainCount = dishs.SelectSingleNode("//span[(@class='dish-price')]").InnerText;
                var prices = dishs.ChildNodes.SelectMany(t => t.Descendants("span")).Select(t => t.InnerText).ToList();

                //var price1 = dishs.ChildNodes.SelectMany(t => t.Attributes["span"].Value).Select(t => t.InnerText).ToList();

            }

            var a = doc;
        }
    }
}