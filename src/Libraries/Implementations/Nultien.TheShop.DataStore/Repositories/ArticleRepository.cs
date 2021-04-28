using Nultien.TheShop.Common.Exceptions;
using Nultien.TheShop.Common.Metrics;
using Nultien.TheShop.Common.Models;
using System;

namespace Nultien.TheShop.DataStore.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly InMemoryDbContext context;
        private readonly ArticleMetrics articleMetrics;

        public ArticleRepository(InMemoryDbContext context, ArticleMetrics articleMetrics)
        {
            this.context = context;
            this.articleMetrics = articleMetrics;
        }

        public Article GetByCode(string code)
        {
            var article = context.Articles.GetByCode(code);
            return article ?? throw new ArticleNotFoundException($"Article {code} not found in database.", articleMetrics);
        }

        public void Add(Article article)
        {
            if (string.IsNullOrEmpty(article.Id))
            {
                article.Id = Guid.NewGuid().ToString();
            }

            context.Articles.Add(article);
        }
        public Article Remove(string code)
        {
            var article = GetByCode(code);

            if (article != null)
            {
                context.Articles.Remove(article);
            }

            return article;
        }

        public void Upsert(Article article)
        {
            Remove(article.Code);
            Add(article);
        }
    }
}
