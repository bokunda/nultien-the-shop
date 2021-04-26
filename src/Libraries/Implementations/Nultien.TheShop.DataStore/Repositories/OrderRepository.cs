using Nultien.TheShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nultien.TheShop.DataStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly InMemoryDbContext context;
        private readonly IInventoryRepository inventoryRepository;

        public OrderRepository(InMemoryDbContext context, IInventoryRepository inventoryRepository)
        {
            this.context = context;
            this.inventoryRepository = inventoryRepository;
        }

        public Order CreateOrder(Inventory inventory, long buyerId)
        {
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
            };

            //foreach (var item in inventory)
            //{
            var item = inventory;
                order.Items.Add(new OrderItem
                {
                    Id = Guid.NewGuid().ToString(),
                    ArticleCode = item.ArticleCode,
                    Price = item.Price,
                    Quantity = 1
                });
            //}

            // Save order in DB
            inventoryRepository.DecreaseQuantity(inventory);
            Add(order);

            return order;
        }

        public void Add(Order order)
        {
            context.Orders.Add(order);
        }
    }
}
