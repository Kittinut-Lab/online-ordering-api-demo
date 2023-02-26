using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Filters;

namespace online_ordering_api
{
    public class Program
    {
      
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //add Logger
                .UseSerilog((context, LoggerConfiguration) => LoggerConfiguration
                .WriteTo.Console()
                .ReadFrom.Configuration(context.Configuration)
                .WriteTo.File($"{ Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "log-.txt") }", rollingInterval: RollingInterval.Day)
                )
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                }).UseWindowsService();

    }
}
