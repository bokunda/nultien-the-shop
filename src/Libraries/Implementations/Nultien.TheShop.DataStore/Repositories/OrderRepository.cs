using Nultien.TheShop.Common.Models;
using Nultien.TheShop.IDataStore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nultien.TheShop.DataStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly InMemoryDbContext context;
        private readonly IInventoryRepository inventoryRepository;
        private readonly ICustomerRepository customerRepository;

        public OrderRepository(InMemoryDbContext context, IInventoryRepository inventoryRepository, ICustomerRepository customerRepository)
        {
            this.context = context;
            this.inventoryRepository = inventoryRepository;
            this.customerRepository = customerRepository;
        }

        public List<OrderItem> CreateOrderItem(List<Inventory> inventories, long quantity)
        {
            var orderItems = new List<OrderItem>();

            foreach(var inventory in inventories)
            {
                if (quantity <= 0) break;
                else
                {
                    var orderItem = new OrderItem
                    {
                        Id = Guid.NewGuid().ToString(),
                        ArticleCode = inventory.ArticleCode,
                        Price = inventory.Price,                        
                    };

                    // If cannot get all resources from one inventory we will take all resources for one order item,
                    // so we are creating order items for every inventory until we collect full quantity.
                    var decr = inventory.Quantity >= quantity ? quantity : inventory.Quantity;
                    orderItem.Quantity = inventory.Quantity;
                    quantity -= inventory.Quantity;
                    orderItem.InventoryId = inventory.Id;

                    orderItems.Add(orderItem);
                    inventoryRepository.DecreaseQuantity(inventory.Id, decr);
                }
            }

            // Check are we collected all items (quantity <= 0)
            // if not, transaction failed, return all items back to inventory
            if (quantity > 0)
            {
                foreach(var orderItem in orderItems)
                {
                    inventoryRepository.IncreaseQuantity(orderItem.InventoryId, orderItem.Quantity);
                }

                return null;
            }

            return orderItems;
        }

        public void Add(Order order)
        {
            context.Orders.Add(order);
        }

        public Order CreateOrder(List<OrderItem> orderItems, string buyerId)
        {
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                Items = orderItems,
                TotalPrice = orderItems.Sum(x => x.Price * x.Quantity),
            };           

            // Save order in DB
            Add(order);
            customerRepository.AssignOrderToCustomer(order, buyerId);

            return order;
        }
    }
}
