using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Models;
using System;
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
