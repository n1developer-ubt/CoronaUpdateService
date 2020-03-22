using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoronaDataScraper;
using HtmlAgilityPack;

namespace CoronaUpdateService.Services
{
    public class ScrapeDataService
    {
        private static string WebSiteToScrape = "https://www.worldometers.info/coronavirus/";
        public static List<CountryData> GetNewData()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(WebSource(WebSiteToScrape));

            HtmlNode table = doc.DocumentNode.SelectSingleNode("//*[@id=\"main_table_countries_today\"]");

            HtmlNode rows = table.SelectSingleNode("//tbody[1]");

            List<HtmlNode> td = rows.Descendants().Where((e) => e.Name == "tr").ToList();

            List<CountryData> data = new List<CountryData>();

            foreach (HtmlNode node in td)
            {
                CountryData newData = new CountryData();
                newData.Load(node);
                data.Add(newData);
                //newData.Print();
            }

            return data;
        }
        private static string WebSource(string website)
        {
            string source = null;
            using (WebClient client = new WebClient())
            {
                source = client.DownloadString(website);
            }

            return source;
        }

    }
}
