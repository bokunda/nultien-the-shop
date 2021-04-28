using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nultien.TheShop.DataStore;
using Nultien.TheShop.DataStore.Repositories;
using Nultien.TheShop.IDataStore;
using Nultien.TheShop.Services;
using Serilog;
using System;
using System.IO;

namespace Nultien.TheShop.Application
{
    public static class Startup
    {
        public static IHost Start()
        {
            // Register unhandled exception trapper
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            // Create builder
            var builder = new ConfigurationBuilder();

            // Use configuration from appsettings.json
            BuildConfig(builder);

            // Configure serilog
            ConfigureLogger(builder);

            Log.Logger.Information("Application Starting!");

            // Configure services
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Services
                    services.AddTransient<IShopService, ShopService>();
                    services.AddTransient<IOrderService, OrderService>();

                    // Repositories
                    services.AddTransient<ISupplierRepository, SupplierRepository>();
                    services.AddTransient<IInventoryRepository, InventoryRepository>();
                    services.AddTransient<IArticleRepository, ArticleRepository>();
                    services.AddTransient<IOrderRepository, OrderRepository>();
                    services.AddTransient<ICustomerRepository, CustomerRepository>();

                    // Singletons
                    services.AddSingleton<InMemoryDbContext>();
                })
                .UseSerilog()
                .Build();

            Log.Logger.Information("Application Started!");

            return host;
        }

        private static void ConfigureLogger(IConfigurationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .CreateLogger();
        }

        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "local"}.json", optional: true)
                .AddEnvironmentVariables();
        }

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Fatal(e.ExceptionObject.ToString());
            Environment.Exit(-1);
        }
    }
}
