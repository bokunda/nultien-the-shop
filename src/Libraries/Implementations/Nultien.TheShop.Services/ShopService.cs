using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Exceptions;
using Nultien.TheShop.Common.Metrics;
using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore.Repositories;
using System;
using System.Collections.Generic;

namespace Nultien.TheShop.Services
{
    public class ShopService : IShopService
    {
        private readonly IOrderService orderService;
        private readonly IArticleRepository articleRepository;
        private readonly OrderMetrics orderMetrics;
        private readonly ArticleMetrics articleMetrics;
        private readonly ILogger<ShopService> logger;

        public ShopService(IOrderService orderService, 
            IArticleRepository articleRepository, 
            OrderMetrics orderMetrics, 
            ArticleMetrics articleMetrics, 
            ILogger<ShopService> logger)
        {
            this.orderService = orderService;
            this.articleRepository = articleRepository;
            this.orderMetrics = orderMetrics;
            this.articleMetrics = articleMetrics;
            this.logger = logger;
        }

        public Article GetArticleInformation(string articleCode)
        {
            try
            {
                var article = articleRepository.GetByCode(articleCode);
                articleMetrics.IncreaseFound();
            }
            catch (ArticleNotFoundException ex)
            {
                logger.LogWarning(ex, "Article {articleCode} not found.", articleCode);
            }

            return null;
        }

        public List<OrderItem> SellArticle(string articleCode, long quantity, float maxExpectedPrice)
        {
            return orderService.OrderArticle(articleCode, quantity, maxExpectedPrice);
        }

        public void CompleteOrder(List<OrderItem> orderItems, string buyerId)
        {
            try
            {
                var order = orderService.CreateOrder(orderItems, buyerId);
                orderMetrics.IncreaseCompleted();
                logger.LogInformation("Order for customer {buyerId} is created, total items: {totalItems}, total price: {totalPrice}.", buyerId, order?.Items?.Count, order?.TotalPrice);
            }
            catch (OrderCreationFailedException ex)
            {
                logger.LogWarning(ex, "Order creation failed. {errorMessage}", ex.ErrorMessage);
            }
        }
    }
}
