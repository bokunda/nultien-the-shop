using Nultien.TheShop.Common.Models;

namespace Nultien.TheShop.DataStore.Repositories
{
    public interface IArticleRepository
    {
        Article GetByCode(string code);
        void Add(Article article);
        void Remove(string code);
        void Upsert(Article article);
    }
}
