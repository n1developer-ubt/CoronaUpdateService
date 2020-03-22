using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaDataScraper;

namespace CoronaUpdateService.Objects
{
    public class AllData
    {
        public List<CountryData> CountryData { get; set; }
        public string TotalCases { get; set; }
        public string NewCases { get; set; }
        public string TotalDeaths { get; set; }
        public string NewDeaths { get; set; }
        public string TotalRecovered { get; set; }
        public string ActiveCases { get; set; }
        public string Serious { get; set; }
        public string TotCases { get; set; }
    }
}
