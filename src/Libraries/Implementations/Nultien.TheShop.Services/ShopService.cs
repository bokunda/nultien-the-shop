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

        public ShopService(IOrderService orderService, IArticleRepository articleRepository)
        {
            this.orderService = orderService;
            this.articleRepository = articleRepository;
        }

        public Article GetArticleInformation(string articleCode)
        {
            return articleRepository.GetByCode(articleCode);
        }

        public List<OrderItem> SellArticle(string articleCode, long quantity, float maxExpectedPrice)
        {
            return orderService.OrderArticle(articleCode, quantity, maxExpectedPrice);
        }

        public Order CompleteOrder(List<OrderItem> orderItems, string buyerId)
        {
            return orderService.CreateOrder(orderItems, buyerId);
        }
    }
}
