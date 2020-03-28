using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoronaDataScraper;
using CoronaUpdateService.Objects;
using HtmlAgilityPack;

namespace CoronaUpdateService.Services
{
    public class ScrapeDataService
    {
        private static string WebSiteToScrape = "http://www.worldometers.info/coronavirus/";
        public static AllData GetNewData()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(WebSource(WebSiteToScrape));

            HtmlNode table = doc.DocumentNode.SelectSingleNode("//*[@id=\"main_table_countries_today\"]");

            HtmlNode rows = table.SelectSingleNode("//tbody[1]");

            List<HtmlNode> td = rows.Descendants().Where((e) => e.Name == "tr").ToList();

            AllData data = new AllData();
            data.CountryData = new List<CountryData>();

            foreach (HtmlNode node in td)
            {
                CountryData newData = new CountryData();
                newData.Load(node);
                data.CountryData.Add(newData);
            }

            List<HtmlNode> totalData =
                doc.DocumentNode.SelectSingleNode("//*[@id=\"main_table_countries_today\"]/tbody[2]/tr").Descendants().Where((e)=>e.Name == "td").ToList();

            data.TotalCases = totalData[1].InnerText.Trim();
            data.NewCases = totalData[2].InnerText.Trim();
            data.TotalDeaths= totalData[3].InnerText.Trim();
            data.NewDeaths= totalData[4].InnerText.Trim();
            data.TotalRecovered= totalData[5].InnerText.Trim();
            data.ActiveCases= totalData[6].InnerText.Trim();
            data.Serious= totalData[7].InnerText.Trim();
            data.TotCases= totalData[8].InnerText.Trim();

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
