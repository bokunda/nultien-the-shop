using Nultien.TheShop.Common;
using Nultien.TheShop.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Nultien.TheShop.DataStore.Wrappers
{
    public class InventoryList : List<Inventory>
    {
        private readonly Dictionary<string, List<Inventory>> inventoryIdIndex = new Dictionary<string, List<Inventory>>();
        private readonly Dictionary<string, List<Inventory>> inventoryArticleCodeIndex = new Dictionary<string, List<Inventory>>();

        public new void Add(Inventory inventory)
        {
            if (inventory == null) return;

            inventoryIdIndex.AddOrUpdate(inventory.Id,
                index =>
                {
                    index.Add(inventory);
                    return index;
                },
                () => new List<Inventory> { inventory });

            inventoryArticleCodeIndex.AddOrUpdate(inventory.ArticleCode,
                index =>
                {
                    index.Add(inventory);
                    return index;
                },
                () => new List<Inventory> { inventory });

            base.Add(inventory);
        }
        
        public List<Inventory> GetByArticleCode(string articleCode)
        {
            List<Inventory> inventory = new List<Inventory>();
            var success = inventoryArticleCodeIndex.TryGetValue(articleCode, out var inventories);

            if (success)
            {
                return inventories;
            }
            else
            {
                inventory = FindAll(x => x.ArticleCode.Equals(articleCode));
                if (inventory != null && inventory.Any())
                {
                    inventoryArticleCodeIndex.AddOrUpdate(inventory.First().ArticleCode,
                    index => { },
                    () => inventory);
                }
            }
            return inventory;
        }

        public List<Inventory> GetByInventoryId(string inventoryId)
        {
            List<Inventory> inventory = new List<Inventory>();
            var success = inventoryIdIndex.TryGetValue(inventoryId, out var inventories);

            if (success)
            {
                return inventories;
            }
            else
            {
                inventory = FindAll(x => x.Id.Equals(inventoryId));
                if (inventory != null && inventory.Any())
                {
                    inventoryIdIndex.AddOrUpdate(inventory.First().ArticleCode,
                    index => { },
                    () => inventory);
                }
            }
            return inventory;
        }
    }
}
