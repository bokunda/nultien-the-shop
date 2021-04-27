using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore.Repositories;
using System.Collections.Generic;
using System.Linq;

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

        public List<OrderItem> OrderArticle(string articleCode, long quantity, float maxExpectedPrice)
        {
            var inventories = inventoryRepository.GetArticleFromInventory(x => x.ArticleCode.Equals(articleCode) && x.Price <= maxExpectedPrice)
                .OrderBy(y => y.Price)
                .ToList();

            var orderItems = orderRepository.CreateOrderItem(inventories, quantity);

            return orderItems;
        }

        public Order CreateOrder(List<OrderItem> orderItems, string buyerId)
        {
            return orderRepository.CreateOrder(orderItems, buyerId);
        }
    }
}
