using Microsoft.Extensions.DependencyInjection;
using Nultien.TheShop.Application;
using Nultien.TheShop.Common.Metrics;
using Nultien.TheShop.DataStore;
using Nultien.TheShop.Services;

namespace Nultien.TheShop.Tests.Unit.Common
{
    public class BaseTestsSetup
    {
        public BaseTestsSetup()
        {
            // Application setup and start
            var host = Startup.Start();

            // Insert data
            Context = DataSetup.InsertData(host.Services);

            // Get instances via DI
            ShopService = ActivatorUtilities.GetServiceOrCreateInstance<IShopService>(host.Services);
            OrderMetrics = ActivatorUtilities.GetServiceOrCreateInstance<OrderMetrics>(host.Services);
            ArticleMetrics = ActivatorUtilities.GetServiceOrCreateInstance<ArticleMetrics>(host.Services);
        }

        public IShopService ShopService { get; set; }
        public OrderMetrics OrderMetrics { get; set; }
        public ArticleMetrics ArticleMetrics { get; set; }
        public InMemoryDbContext Context { get; set; }
    }
}
