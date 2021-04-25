using Microsoft.Extensions.Logging;

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
    }
}
