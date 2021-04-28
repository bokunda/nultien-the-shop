using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Enums;
using Nultien.TheShop.Common.Exceptions;
using Nultien.TheShop.Common.Metrics;
using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Nultien.TheShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> logger;
        private readonly IInventoryRepository inventoryRepository;
        private readonly IOrderRepository orderRepository;
        private readonly OrderMetrics orderMetrics;

        public OrderService(IInventoryRepository inventoryRepository,
            IOrderRepository orderRepository,
            OrderMetrics orderMetrics,
            ILogger<OrderService> logger)
        {
            this.inventoryRepository = inventoryRepository;
            this.orderRepository = orderRepository;
            this.orderMetrics = orderMetrics;
            this.logger = logger;
        }

        /// </<inheritdoc/>
        public List<OrderItem> OrderArticle(string articleCode, long quantity, double maxExpectedPrice)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            var inventories = inventoryRepository.GetArticleFromInventory(InventoryIndexType.ArticleCode, string.Empty, articleCode, x => x.Price <= maxExpectedPrice)
                .OrderBy(y => y.Price)
                .ToList();

            logger.LogInformation("OrderArticle: Got {totalInventories} from DB for article {articleCode} where price is equal or lower than {maxExpectedPrice}.", inventories?.Count, articleCode, maxExpectedPrice);

            if (inventories?.Count > 0)
            {
                orderItems = orderRepository.CreateOrderItem(inventories, quantity);
            }

            return orderItems;
        }

        /// </<inheritdoc/>
        public Order CreateOrder(List<OrderItem> orderItems, string buyerId)
        {
            if (orderItems != null && orderItems.Any())
            {
                return orderRepository.CreateOrder(orderItems, buyerId);
            }
            else
            {
                var msg = $"Order for buyer {buyerId} cannot be created because there are no order items.";
                throw new OrderCreationFailedException(msg, orderMetrics);
            }

        }
    }
}
