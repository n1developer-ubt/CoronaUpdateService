using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HtmlAgilityPack;

namespace CoronaDataScraper
{
    
    public class CountryData
    {
        public string Name { get; set; }
        public int TotalCases { get; set; }
        public int NewCases { get; set; }
        public int TotalDeaths { get; set; }
        public int NewDeaths { get; set; }
        public int TotalRecovered { get; set; }
        public int ActiveCases { get; set; }
        public int Serious { get; set; }
        public double TotCases { get; set; }

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

            TotalCases= FilterDigit(col[1].InnerText).Item1;
            NewCases= FilterDigit(col[2].InnerText).Item1;
            TotalDeaths= FilterDigit(col[3].InnerText).Item1;
            NewDeaths= FilterDigit(col[4].InnerText).Item1;
            TotalRecovered= FilterDigit(col[5].InnerText).Item1;
            ActiveCases= FilterDigit(col[6].InnerText).Item1;
            Serious= FilterDigit(col[7].InnerText).Item1;
            TotCases= FilterDigit(col[8].InnerText, true).Item2;

            return true;
        }
        private (int,double) FilterDigit(string d, bool isFloat = false)
        {
            string str = "";
            bool once = false;
            foreach (char c in d)
            {
                if (Char.IsDigit(c))
                    str += c;
                if (isFloat && !once && c == '.')
                {
                    str += '.';
                    once = true;
                }
            }

            if (str.Trim().Equals(""))
                return (0,0);
            if (isFloat)
                return (0, Convert.ToDouble(str));

            return (Convert.ToInt32(str),0) ;
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
