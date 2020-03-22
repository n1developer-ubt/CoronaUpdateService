using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace CoronaDataScraper
{
    class Program
    {
        private static string WebSiteToScrape = "https://www.worldometers.info/coronavirus/";
        static void Main(string[] args)
        {
            //string source = WebSource(WebSiteToScrape);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(File.ReadAllText("data.html"));

            HtmlNode table = doc.DocumentNode.SelectSingleNode("//*[@id=\"main_table_countries_today\"]");

            HtmlNode rows = table.SelectSingleNode("//tbody[1]");

            List<HtmlNode> td = rows.Descendants().Where((e) => e.Name=="tr").ToList();

            List<CountryData> data = new List<CountryData>();

            foreach (HtmlNode node in td)
            {
                CountryData newData = new CountryData();
                newData.Load(node);
                data.Add(newData);
                newData.Print();
            }
            Console.WriteLine(data.Count);
            Console.ReadKey();
            Console.WriteLine("Hello World!");
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
