using Nultien.TheShop.Common.Models;
using System.Collections.Generic;

namespace Nultien.TheShop.DataStore.Repositories
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Creates order items for the given articles from inventory.
        /// </summary>
        /// <param name="inventories"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        List<OrderItem> CreateOrderItem(List<Inventory> inventories, long quantity);

        /// <summary>
        /// Creates order based on order items and customer id.
        /// </summary>
        /// <param name="orderItems"></param>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        Order CreateOrder(List<OrderItem> orderItems, string buyerId);

        /// <summary>
        /// Save order in the database.
        /// </summary>
        /// <param name="order"></param>
        void Add(Order order);
    }
}
