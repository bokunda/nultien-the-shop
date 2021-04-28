using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ArticleRepository> logger;

        public ArticleRepository(InMemoryDbContext context, ArticleMetrics articleMetrics, ILogger<ArticleRepository> logger)
        {
            this.context = context;
            this.articleMetrics = articleMetrics;
            this.logger = logger;
        }

        /// </<inheritdoc/>
        public Article GetByCode(string code)
        {
            var article = context.Articles.GetByCode(code);
            return article ?? throw new ArticleNotFoundException($"Article {code} not found in database.", articleMetrics);
        }

        /// </<inheritdoc/>
        public void Add(Article article)
        {
            if (string.IsNullOrEmpty(article.Id))
            {
                article.Id = Guid.NewGuid().ToString();
            }

            if (!context.Articles.Contains(article))
            {
                context.Articles.Add(article);
            }
            else
            {
                logger.LogInformation("Article {articleCode} already exists.", article.Code);
            }
        }

        /// </<inheritdoc/>
        public Article Remove(string code)
        {
            var article = GetByCode(code);

            if (article != null)
            {
                context.Articles.Remove(article);
            }

            return article;
        }
    }
}
