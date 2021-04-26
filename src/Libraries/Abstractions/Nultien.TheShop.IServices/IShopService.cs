using Nultien.TheShop.Common.Models;

namespace Nultien.TheShop.Services
{
    public interface IShopService
    {
        Article GetArticleInformation(string articleCode);
        Order SellArticle(string articleCode, float maxExpectedPrice, long buyerId);
    }
}
