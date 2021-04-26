using Microsoft.Extensions.DependencyInjection;
using Nultien.TheShop.Services;

namespace Nultien.TheShop.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Application setup and start
            var host = Startup.Start();

            // Get instance
            var shopService = ActivatorUtilities.CreateInstance<ShopService>(host.Services);
            shopService.SellArticle("article-code-123", 100, 1);
            shopService.GetArticleInformation("article-code-123");
        }
    }
}
