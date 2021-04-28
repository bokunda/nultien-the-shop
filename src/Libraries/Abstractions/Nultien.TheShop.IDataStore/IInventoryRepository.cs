using Nultien.TheShop.Common.Enums;
using Nultien.TheShop.Common.Models;
using System;
using System.Collections.Generic;

namespace Nultien.TheShop.DataStore.Repositories
{
    public interface IInventoryRepository
    {
        /// <summary>
        /// Gets article from inventory based on database index and delegate.
        /// </summary>
        /// <param name="indexType"></param>
        /// <param name="inventoryId"></param>
        /// <param name="articleCode"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        List<Inventory> GetArticleFromInventory(InventoryIndexType indexType, string inventoryId, string articleCode, Func<Inventory, bool> func);
        
        /// <summary>
        /// Decrease article quantity in given inventory
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="decrement"></param>
        /// <returns></returns>
        bool DecreaseQuantity(string inventoryId, long decrement = 1);

        /// <summary>
        /// Increase article quantity in given inventory
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        bool IncreaseQuantity(string inventoryId, long increment = 1);
    }
}