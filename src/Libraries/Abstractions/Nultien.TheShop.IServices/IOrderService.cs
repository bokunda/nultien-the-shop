using Nultien.TheShop.Common.Models;
using System.Collections.Generic;

namespace Nultien.TheShop.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Takes article from the inventories and creates order items.
        /// </summary>
        /// <param name="articleCode"></param>
        /// <param name="quantity"></param>
        /// <param name="maxExpectedPrice"></param>
        /// <returns></returns>
        List<OrderItem> OrderArticle(string articleCode, long quantity, double maxExpectedPrice);

        /// <summary>
        /// Create order based on order items and buyer id.
        /// </summary>
        /// <param name="orderItems"></param>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        Order CreateOrder(List<OrderItem> orderItems, string buyerId);
    }
}
