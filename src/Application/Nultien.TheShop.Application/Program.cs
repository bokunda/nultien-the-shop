using Microsoft.Extensions.DependencyInjection;
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
            var context = DataSetup.InsertData(host.Services);

            // Get shopService instance
            var shopService = ActivatorUtilities.GetServiceOrCreateInstance<IShopService>(host.Services);
            

            var orderItems = shopService.SellArticle("123-456-789", 10, 1000);
            var order = shopService.CompleteOrder(orderItems, context.Customers.First().Id);

            var article = shopService.GetArticleInformation("111-222-333");
        }
    }
}
