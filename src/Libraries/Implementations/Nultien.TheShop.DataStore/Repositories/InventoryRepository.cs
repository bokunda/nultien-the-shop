using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Models;
using System;
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

        public Inventory GetArticleFromInventory(Func<Inventory, bool> func)
        {
            return context.Inventories.Where(func).FirstOrDefault();
        }

        public bool DecreaseQuantity(string inventoryId, long decrement = 1)
        {
            var dbInventory = GetArticleFromInventory(x => x.Id.Equals(inventoryId));

            if (dbInventory.Quantity > 0)
            {
                dbInventory.Quantity -= decrement;
                return true;
            }

            return false;
        }

        public bool IncreaseQuantity(string inventoryId, long increment = 1)
        {
            var dbInventory = GetArticleFromInventory(x => x.Id.Equals(inventoryId));            
            dbInventory.Quantity += increment;

            return true;
        }
    }
}
