using Nultien.TheShop.Common.Models;
using System;

namespace Nultien.TheShop.DataStore.Repositories
{
    public interface IInventoryRepository
    {
        Inventory GetArticleFromInventory(Func<Inventory, bool> func);
        bool DecreaseQuantity(string inventoryId, long decrement = 1);
        bool IncreaseQuantity(string inventoryId, long increment = 1);
    }
}