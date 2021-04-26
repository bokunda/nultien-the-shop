using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Models;
using System.Linq;

namespace Nultien.TheShop.DataStore.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly InMemoryDbContext context;
        private readonly ILogger<ArticleRepository> logger;

        public ArticleRepository(InMemoryDbContext context, ILogger<ArticleRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Article GetByCode(string code)
        {
            return context.Articles.FirstOrDefault(x => x.Code.Equals(code));
        }
    }
}
