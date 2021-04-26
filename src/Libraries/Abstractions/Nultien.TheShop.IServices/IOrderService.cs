using Nultien.TheShop.Common.Models;

namespace Nultien.TheShop.Services
{
    public interface IOrderService
    {
        Order OrderArticle(string articleCode, float maxExpectedPrice, long buyerId);
    }
}
