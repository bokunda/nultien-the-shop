using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore.Repositories;
using System;
using System.Collections.Generic;

namespace Nultien.TheShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> logger;
        private readonly ISupplierRepository supplierRepository;
        private readonly IInventoryRepository inventoryRepository;
        private readonly IArticleRepository articleRepository;
        private readonly IOrderRepository orderRepository;

        public OrderService(ISupplierRepository supplierRepository, 
            IInventoryRepository inventoryRepository,
            IOrderRepository orderRepository,
            IArticleRepository articleRepository, 
            ILogger<OrderService> logger)
        {
            this.supplierRepository = supplierRepository;
            this.articleRepository = articleRepository;
            this.inventoryRepository = inventoryRepository;
            this.orderRepository = orderRepository;
            this.logger = logger;
        }

        public Order OrderArticle(string articleCode, float maxExpectedPrice, long buyerId)
        {
            var inventory = inventoryRepository.GetArticle(x => x.ArticleCode.Equals(articleCode) && x.Price <= maxExpectedPrice);
            return orderRepository.CreateOrder(inventory, buyerId);
        }
    }
}
