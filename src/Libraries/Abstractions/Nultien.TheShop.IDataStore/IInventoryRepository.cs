using Nultien.TheShop.Common.Models;
using System;

namespace Nultien.TheShop.DataStore.Repositories
{
    public interface IInventoryRepository
    {
        Inventory GetArticle(Func<Inventory, bool> func);
        bool DecreaseQuantity(Inventory inventory, long decrement = 1);
    }
}