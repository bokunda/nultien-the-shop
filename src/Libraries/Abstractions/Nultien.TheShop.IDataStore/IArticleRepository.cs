using Nultien.TheShop.Common.Models;

namespace Nultien.TheShop.DataStore.Repositories
{
    public interface IArticleRepository
    {
        /// <summary>
        /// Gets article by article code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Article GetByCode(string code);

        /// <summary>
        /// Adds article in the database
        /// </summary>
        /// <param name="article"></param>
        void Add(Article article);

        /// <summary>
        /// Removes article by article code from the database
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Article Remove(string code);
    }
}
