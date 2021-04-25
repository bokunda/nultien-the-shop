using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using Serilog;

namespace Nultien.TheShop.Application
{
    public static class Startup
    {
        public static void Start()
        {
            var builder = new ConfigurationBuilder();

            BuildConfig(builder);
            ConfigureLogger(builder);

            Log.Logger.Information("Application Starting!");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {

                })
                .UseSerilog()
                .Build();

            Log.Logger.Information("Application Started!");
        }

        private static void ConfigureLogger(IConfigurationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "local"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
