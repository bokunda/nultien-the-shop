using Autofac.Extras.Moq;
using Microsoft.Extensions.Logging;
using Moq;
using Nultien.TheShop.Common.Exceptions;
using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore;
using Nultien.TheShop.DataStore.Repositories;
using Nultien.TheShop.Services;
using System;
using System.Linq;
using Xunit;

namespace Nultien.TheShop.Tests.Unit.DataStore
{
    public class ArticleRepositoryTests
    {
        [Fact]
        public void ArticleRepository_HappyFlow()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var article = new Article { Id = Guid.NewGuid().ToString(), Name = "Article 1", Code = "111-222-333", Description = "Description" };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Articles.Add(article);

                mock.Mock<ILogger<ArticleRepository>>();

                var articleRepository = mock.Create<ArticleRepository>();

                // Act
                var actual = articleRepository.GetByCode(article.Code);

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(actual.Code, article.Code);
            }
        }

        [Fact]
        public void ArticleRepository_ArticleDoesntExists()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var article = new Article { Id = Guid.NewGuid().ToString(), Name = "Article 1", Code = "111-222-333", Description = "Description" };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                mock.Mock<ILogger<ArticleRepository>>();

                var articleRepository = mock.Create<ArticleRepository>();

                // Act
                // Assert
                Assert.Throws<ArticleNotFoundException>(() => articleRepository.GetByCode(article.Code));
            }
        }

        [Fact]
        public void ArticleRepository_AddArticle()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var article = new Article { Id = Guid.NewGuid().ToString(), Name = "Article 1", Code = "111-222-333", Description = "Description" };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                mock.Mock<ILogger<ArticleRepository>>();

                var articleRepository = mock.Create<ArticleRepository>();

                // Act
                articleRepository.Add(article);

                // Assert
                Assert.Single(context.Articles);
            }
        }

        [Fact]
        public void ArticleRepository_AddArticleTwice()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var article = new Article { Name = "Article 1", Code = "111-222-333", Description = "Description" };
                var article2 = new Article { Name = "Article 1", Code = "111-222-333", Description = "Description" };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                mock.Mock<ILogger<ArticleRepository>>();

                var articleRepository = mock.Create<ArticleRepository>();

                // Act
                articleRepository.Add(article);
                articleRepository.Add(article2);

                // Assert
                Assert.Single(context.Articles);
            }
        }

        [Fact]
        public void ArticleRepository_Remove()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var article = new Article { Name = "Article 1", Code = "111-222-333", Description = "Description" };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                mock.Mock<ILogger<ArticleRepository>>();

                var articleRepository = mock.Create<ArticleRepository>();
                articleRepository.Add(article);

                // Act
                articleRepository.Remove(article.Code);

                // Assert
                Assert.True(!context.Articles.Any());
            }
        }

        [Fact]
        public void ArticleRepository_RemoveNegative()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var article = new Article { Name = "Article 1", Code = "111-222-333", Description = "Description" };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                mock.Mock<ILogger<ArticleRepository>>();

                var articleRepository = mock.Create<ArticleRepository>();

                // Act
                // Assert
                Assert.Throws<ArticleNotFoundException>(() => articleRepository.Remove("INVALID"));
            }
        }
    }
}
