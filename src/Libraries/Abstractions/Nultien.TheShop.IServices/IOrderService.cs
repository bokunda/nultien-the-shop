using Nultien.TheShop.Common.Models;
using System.Collections.Generic;

namespace Nultien.TheShop.Services
{
    public interface IOrderService
    {
        List<OrderItem> OrderArticle(string articleCode, long quantity, float maxExpectedPrice);
        Order CreateOrder(List<OrderItem> orderItems, string buyerId);
    }
}
