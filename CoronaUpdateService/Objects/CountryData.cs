using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HtmlAgilityPack;

namespace CoronaDataScraper
{
    
    class CountryData
    {
        public string Name { get; set; }
        public string TotalCases { get; set; }
        public string NewCases { get; set; }
        public string TotalDeaths { get; set; }
        public string NewDeaths { get; set; }
        public string TotalRecovered { get; set; }
        public string ActiveCases { get; set; }
        public string Serious { get; set; }
        public string TotCases { get; set; }

        public bool Load(HtmlNode row)
        {
            List<HtmlNode> col = row.Descendants().Where((d)=>d.Name == "td").ToList();

            try
            {
                Name = col[0].SelectSingleNode("a").InnerText;
            }
            catch (Exception e)
            {
                Name = col[0].InnerText.Trim();
            }

            TotalCases= col[1].InnerText;
            NewCases= col[2].InnerText;
            TotalDeaths= col[3].InnerText;
            NewDeaths= col[4].InnerText;
            TotalRecovered= col[5].InnerText;
            ActiveCases= col[6].InnerText;
            Serious= col[7].InnerText;
            TotCases= col[8].InnerText;

            return true;
        }

        public void Print()
        {
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                Console.WriteLine(property.Name + ":\t" + property.GetValue(this,null));
            }
            Console.WriteLine();
            //Console.WriteLine($"Name: \t" +
            //                  $"TotalCase: \t" +
            //                  $"NewCase: \t" +
            //                  $"TotalDeaths: \t" +
            //                  $"NewDeaths: \t" +
            //                  $"TotalRecovered: \t" +
            //                  $"Active");
        }
    }
}
