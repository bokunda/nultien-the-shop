using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore.Repositories;
using System;

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

        public Order SellArticle(string articleCode, float maxExpectedPrice, long buyerId)
        {
            return orderService.OrderArticle(articleCode, maxExpectedPrice, buyerId);
        }
    }
}
