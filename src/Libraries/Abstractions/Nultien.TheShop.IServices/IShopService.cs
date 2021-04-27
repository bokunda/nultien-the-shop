using Nultien.TheShop.Common.Models;
using System.Collections.Generic;

namespace Nultien.TheShop.Services
{
    public interface IShopService
    {
        Article GetArticleInformation(string articleCode);
        List<OrderItem> SellArticle(string articleCode, long quantity, float maxExpectedPrice);
        Order CompleteOrder(List<OrderItem> orderItems, string buyerId);
    }
}
