using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Enums;
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

        public List<Inventory> GetArticleFromInventory(InventoryIndexType indexType, string inventoryId, string articleCode, Func<Inventory, bool> func)
        {
            var inventoryData = new List<Inventory>();

            if (indexType == InventoryIndexType.ArticleCode)
            {
                inventoryData.AddRange(context.Inventories.GetByArticleCode(articleCode).Where(func));
            }
            else if (indexType == InventoryIndexType.InventoryId)
            {
                inventoryData.AddRange(context.Inventories.GetByInventoryId(inventoryId).Where(func));
            }
            
            return inventoryData;
        }

        public bool DecreaseQuantity(string inventoryId, long decrement = 1)
        {
            var dbInventory = GetArticleFromInventory(InventoryIndexType.InventoryId, inventoryId, string.Empty, x => true).FirstOrDefault();

            if (dbInventory.Quantity > 0)
            {
                dbInventory.Quantity -= decrement;
                return true;
            }

            return false;
        }

        public bool IncreaseQuantity(string inventoryId, long increment = 1)
        {
            var dbInventory = GetArticleFromInventory(InventoryIndexType.InventoryId, inventoryId, string.Empty, x => true).FirstOrDefault();
            dbInventory.Quantity += increment;

            return true;
        }
    }
}
