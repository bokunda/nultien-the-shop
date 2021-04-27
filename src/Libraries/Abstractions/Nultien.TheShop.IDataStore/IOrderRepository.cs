using Nultien.TheShop.Common.Models;
using System.Collections.Generic;

namespace Nultien.TheShop.DataStore.Repositories
{
    public interface IOrderRepository
    {
        List<OrderItem> CreateOrderItem(List<Inventory> inventories, long quantity);
        Order CreateOrder(List<OrderItem> orderItems, string buyerId);
        void Add(Order order);
    }
}
