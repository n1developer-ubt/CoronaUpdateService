using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoronaUpdateService.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace CoronaUpdateService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Thread th = new Thread(() =>
            {
                UpdateDataService service = new UpdateDataService();
                service.Start();
            });
            th.Start();
            CreateHostBuilder(args).Build().Run();
            //"http://localhost:5000", "http://*:80"
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://*:9080", "http://*:9070");
                });
    }
}
