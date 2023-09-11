using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace MKExpress.Web.API
{
    public class Program
    {
        private static string _env = "dev";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static void SetEnvironment()
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", false)
                    .Build();
                _env = config.GetSection("Environment").Value;
            }
            catch (Exception)
            {
                _env = "dev";
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        //.ConfigureAppConfiguration((hostingContext, config) =>
        //        {
        //            config.AddJsonFile("appsettings.json");
        //            config.AddJsonFile($"appsettings.{_env}.json", true);
        //        });
    }
}
