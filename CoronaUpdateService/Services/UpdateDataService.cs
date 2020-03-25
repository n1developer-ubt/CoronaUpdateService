using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using CoronaDataScraper;
using CoronaUpdateService.Objects;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Exception = System.Exception;

namespace CoronaUpdateService.Services
{
    public class UpdateDataService
    {
        private Timer _timer;
        private static readonly string StatusPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Status.txt");
        private static readonly string DataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "data.txt");

        public static string GetUpdatedData()
        {
            string data = "";

            while (true)
            {
                try
                {
                    data = File.ReadAllText(DataPath);
                    return data;
                }
                catch (Exception e)
                {
                    
                }
            }
        }
        public UpdateDataService()
        {
            _timer = new Timer(30*60*1000);
            _timer.Elapsed += TimerOnElapsed;
            _timer.Enabled = true;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateData();
        }

        public void Start()
        {
            System.Diagnostics.Debug.Write("Service Started!");
            _timer.Start();
            UpdateData();
        }

        private void UpdateData()
        {
            while (true)
            {
                try
                {
                    File.WriteAllText(StatusPath, "0");
                    break;
                }
                catch (Exception e)
                {
                    
                }
            }
            AllData updatedData = ScrapeDataService.GetNewData();
            UpdateFile(updatedData);
            while (true)
            {
                try
                {
                    File.WriteAllText(StatusPath, "1");
                    break;
                }
                catch (Exception e)
                {

                }
            }
        }

        public void Stop()
        {
            System.Diagnostics.Debug.Write("Service Stopped!");
            _timer.Stop();
        }
        private void UpdateFile(AllData data)
        {
            string dx = JsonConvert.SerializeObject(data);
            while (true)
            {
                try
                {
                    
                    File.WriteAllText(DataPath, dx);
                    break;
                }
                catch (Exception e)
                {
                    
                }
            }
        }
        private void UpdateDatabase(List<CountryData> data)
        {
            MySqlConnection con = new MySqlConnection(CurrentStatus.ConnectionString);
            MySqlCommand cmd = con.CreateCommand();
            con.Open();

            cmd.CommandText = $"TRUNCATE {CurrentStatus.TableName}";
            cmd.ExecuteNonQuery();

            string query = "";

            foreach (CountryData c in data)
            {
                if (query.Length == 0)
                {
                    query =
                        $"INSERT INTO NewData(Name, TotalCases, NewCases, TotalDeaths, NewDeaths, TotalRecovered, ActiveCases, Serious, TotCases) VALUES('{c.Name}', {FilterDigit(c.TotalCases)}, {FilterDigit(c.NewCases)}, {FilterDigit(c.TotalDeaths)}, {FilterDigit(c.NewDeaths)}, {FilterDigit(c.TotalRecovered)}, {FilterDigit(c.ActiveCases)}, {FilterDigit(c.Serious)}, {FilterDigit(c.TotCases)})";
                    ;
                }
                else
                {
                    query +=
                        $",('{c.Name}', {FilterDigit(c.TotalCases)}, {FilterDigit(c.NewCases)}, {FilterDigit(c.TotalDeaths)}, {FilterDigit(c.NewDeaths)}, {FilterDigit(c.TotalRecovered)}, {FilterDigit(c.ActiveCases)}, {FilterDigit(c.Serious)}, {FilterDigit(c.TotCases)})";
                }
            }

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public double FilterDigit(double d)
        {
            return d;
        }
        public int FilterDigit(int d)
        {
            return d;
        }
    }
}
