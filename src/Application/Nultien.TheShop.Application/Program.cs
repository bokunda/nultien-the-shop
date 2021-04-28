using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Exceptions;
using Nultien.TheShop.Common.Metrics;
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

            // Get instances to work with
            var shopService = ActivatorUtilities.GetServiceOrCreateInstance<IShopService>(host.Services);
            var orderMetrics = ActivatorUtilities.GetServiceOrCreateInstance<OrderMetrics>(host.Services);
            var articleMetrics = ActivatorUtilities.GetServiceOrCreateInstance<ArticleMetrics>(host.Services);
            var logger = ActivatorUtilities.GetServiceOrCreateInstance<ILogger<Program>>(host.Services);


            // Create orders
            var orderItems = shopService.SellArticle("123-456-789", 10, 1000);            
            shopService.CompleteOrder(orderItems, context.Customers.First().Id);

            orderItems = shopService.SellArticle("INVALID", 10, 1000);
            shopService.CompleteOrder(orderItems, context.Customers.First().Id);

            // Article search
            try
            {
                var article = shopService.GetArticleInformation("111-222-333");
                logger.LogInformation(article.ToString());
            }
            catch (ArticleNotFoundException ex)
            {
                logger.LogWarning(ex, "Article not found! {errorMessage}", ex.ErrorMessage);
            }

            try
            {
                var article = shopService.GetArticleInformation("INVALID");
                logger.LogInformation(article.ToString());
            }
            catch (ArticleNotFoundException ex)
            {
                logger.LogWarning(ex, "Article not found! {errorMessage}", ex.ErrorMessage);
            }

            // Metrics
            logger.LogInformation("Metrics for Orders, total orders attempts: {totalOrderAttempts}, successfull orders {successfullOrders}, failed orders {failedOrders}", orderMetrics.Failed + orderMetrics.Completed, orderMetrics.Completed, orderMetrics.Failed);
            logger.LogInformation("Metrics for Articles, total searched articles: {totalArticlesSearches}, articles that exists {existingArticles}, non existsing articles {nonExistingArticles}", articleMetrics.NotFound + articleMetrics.Found, articleMetrics.Found, articleMetrics.NotFound);
        }
    }
}
