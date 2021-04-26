using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nultien.TheShop.DataStore.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InMemoryDbContext context;
        private readonly ILogger<InventoryRepository> logger;

        public InventoryRepository(InMemoryDbContext context, ILogger<InventoryRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Inventory GetArticle(Func<Inventory, bool> func)
        {
            return context.Inventories.Where(func).FirstOrDefault();
        }

        public bool DecreaseQuantity(Inventory inventory, long decrement = 1)
        {
            var dbInventory = context.Inventories.FirstOrDefault(x => x.Id == inventory.Id);

            if (dbInventory.Quantity > 0)
            {
                dbInventory.Quantity -= decrement;
                return true;
            }

            return false;
        }
    }
}
