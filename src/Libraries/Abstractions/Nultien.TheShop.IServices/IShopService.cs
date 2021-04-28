using Nultien.TheShop.Common.Models;
using System.Collections.Generic;

namespace Nultien.TheShop.Services
{
    public interface IShopService
    {
        /// <summary>
        /// Gets article informations based on article code, throws ArticleNotFoundException if article doesnt exists.
        /// </summary>
        /// <param name="articleCode"></param>
        /// <returns></returns>
        Article GetArticleInformation(string articleCode);

        /// <summary>
        /// Take article from inventories and create order items.
        /// </summary>
        /// <param name="articleCode"></param>
        /// <param name="quantity"></param>
        /// <param name="maxExpectedPrice"></param>
        /// <returns></returns>
        List<OrderItem> SellArticle(string articleCode, long quantity, double maxExpectedPrice);

        /// <summary>
        /// Create order based on order items for customer id.
        /// </summary>
        /// <param name="orderItems"></param>
        /// <param name="buyerId"></param>
        void CompleteOrder(List<OrderItem> orderItems, string buyerId);
    }
}
