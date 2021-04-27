using Microsoft.Extensions.DependencyInjection;
using Nultien.TheShop.DataStore;
using Nultien.TheShop.Services;
using System.Linq;

namespace Nultien.TheShop.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Application setup and start
            var host = Startup.Start();

            // Insert data
            var context = ActivatorUtilities.GetServiceOrCreateInstance<InMemoryDbContext>(host.Services);
            DataSetup.InsertData(context);

            // Get shopService instance
            var shopService = ActivatorUtilities.CreateInstance<ShopService>(host.Services);

            var orderItems = shopService.SellArticle("123-456-789", 10, 1000);
            var order = shopService.CompleteOrder(orderItems, context.Customers.First().Id);

            var article = shopService.GetArticleInformation("111-222-333");
        }
    }
}
