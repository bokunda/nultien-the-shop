﻿using Nultien.TheShop.Common;
using Nultien.TheShop.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Nultien.TheShop.DataStore.Wrappers
{
    public class ArticleList : List<Article>
    {
        private readonly Dictionary<string, List<Article>> articleIdIndex = new Dictionary<string, List<Article>>();

        public new void Add(Article article)
        {
            if (article == null) return;

            articleIdIndex.AddOrUpdate(article.Code.ToLower(),
                index =>
                {
                    index.Add(article);
                    return index;
                },
                () => new List<Article> { article });

            base.Add(article);
        }

        public new bool Contains(Article article)
        {
            return !string.IsNullOrEmpty(article.Code)
                ? articleIdIndex.ContainsKey(article.Code.ToLower())
                : base.Contains(article);
        }

        public new bool Remove(Article article)
        {
            var removed = base.Remove(article);

            if (!string.IsNullOrEmpty(article.Code))
            {
                articleIdIndex.Remove(article.Code.ToLower());
            }

            return removed;
        }

        public Article GetByCode(string articleCode)
        {
            Article article;

            var success = articleIdIndex.TryGetValue(articleCode, out var articles);

            if (success)
            {
                article = articles.FirstOrDefault();
            }
            else
            {
                article = base.Find(x => x.Code.Equals(articleCode));
                if (article != null)
                {
                    articleIdIndex.AddOrUpdate(article.Code.ToLower(),
                    index =>
                    {
                        index.Add(article);
                        return index;
                    },
                    () => new List<Article> { article });
                }
            }

            return article;
        }
    }
}
