using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Exceptions;
using Nultien.TheShop.Common.Metrics;
using Nultien.TheShop.DataStore;
using Nultien.TheShop.Services;
using System;
using System.Linq;

namespace Nultien.TheShop.Application
{
    class Program
    {
        private static IShopService shopService;
        private static OrderMetrics orderMetrics;
        private static ArticleMetrics articleMetrics;
        private static InMemoryDbContext context;
        private static ILogger<Program> logger;

        static void Main(string[] args)
        {
            // Application setup and start
            var host = Startup.Start();

            // Insert data
            context = DataSetup.InsertData(host.Services);

            // Get instances to work with
            GetInstancesFromDI(host.Services);

            // Create orders
            CreateOrders();

            // Article search
            SearchForArticles();

            // Log metrics

            // Metrics
            ShowMetrics();
        }

        private static void GetInstancesFromDI(IServiceProvider services)
        {
            shopService = ActivatorUtilities.GetServiceOrCreateInstance<IShopService>(services);
            orderMetrics = ActivatorUtilities.GetServiceOrCreateInstance<OrderMetrics>(services);
            articleMetrics = ActivatorUtilities.GetServiceOrCreateInstance<ArticleMetrics>(services);
            logger = ActivatorUtilities.GetServiceOrCreateInstance<ILogger<Program>>(services);
        }

        private static void ShowMetrics()
        {
            logger.LogInformation("Metrics for Orders, total orders attempts: {totalOrderAttempts}, successfull orders {successfullOrders}, failed orders {failedOrders}", orderMetrics.Failed + orderMetrics.Completed, orderMetrics.Completed, orderMetrics.Failed);
            logger.LogInformation("Metrics for Articles, total searched articles: {totalArticlesSearches}, articles that exists {existingArticles}, non existsing articles {nonExistingArticles}", articleMetrics.NotFound + articleMetrics.Found, articleMetrics.Found, articleMetrics.NotFound);
        }

        private static void SearchForArticles()
        {
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
        }

        private static void CreateOrders()
        {
            var orderItems = shopService.SellArticle("123-456-789", 10, 1000);
            shopService.CompleteOrder(orderItems, context.Customers.First().Id);

            orderItems = shopService.SellArticle("INVALID", 10, 1000);
            shopService.CompleteOrder(orderItems, context.Customers.First().Id);
        }
    }
}
