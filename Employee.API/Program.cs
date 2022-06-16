using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            //Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().ReadFrom.Configuration(config).CreateLogger();

            Log.Logger = new LoggerConfiguration()
                      .Enrich.FromLogContext()
                      .WriteTo.Console()
                      .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
                      .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                      .CreateLogger();

            Log.Information("Application Startup");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
