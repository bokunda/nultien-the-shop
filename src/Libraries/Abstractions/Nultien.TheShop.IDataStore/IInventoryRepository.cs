using Nultien.TheShop.Common.Models;
using System;
using System.Collections.Generic;

namespace Nultien.TheShop.DataStore.Repositories
{
    public interface IInventoryRepository
    {
        IEnumerable<Inventory> GetArticleFromInventory(Func<Inventory, bool> func);
        bool DecreaseQuantity(string inventoryId, long decrement = 1);
        bool IncreaseQuantity(string inventoryId, long increment = 1);
    }
}