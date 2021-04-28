using Autofac.Extras.Moq;
using Microsoft.Extensions.Logging;
using Moq;
using Nultien.TheShop.Common.Exceptions;
using Nultien.TheShop.Common.Metrics;
using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore.Repositories;
using Nultien.TheShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Nultien.TheShop.Tests.Unit.Services
{
    [Collection("Sequential")]
    public class ShopServiceTests
    {
        [Fact]
        public void GetArticleInformation_HappyFlow()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var articleCode = "111-222-333";

                mock.Mock<IArticleRepository>()
                    .Setup(x => x.GetByCode(articleCode))
                    .Returns(new Article { Id = It.IsAny<string>(), Code = articleCode, Name = It.IsAny<string>(), Description = It.IsAny<string>()});

                mock.Mock<ILogger<ShopService>>();

                var shopService = mock.Create<ShopService>();

                // Act
                var actual = shopService.GetArticleInformation(articleCode);

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(actual.Code, articleCode);
            }
        }

        [Fact]
        public void GetArticleInformation_ArticleNotFound()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var articleCode = "111-222-333";

                mock.Mock<IArticleRepository>()
                    .Setup(x => x.GetByCode(articleCode))
                    .Throws(new ArticleNotFoundException("Article not found.", new ArticleMetrics()));

                mock.Mock<ILogger<ShopService>>();

                var shopService = mock.Create<ShopService>();

                // Act
                // Assert
                Assert.Throws<ArticleNotFoundException>(() => shopService.GetArticleInformation(articleCode));
            }
        }

        [Fact]
        public void SellArticle_HappyFlow()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var articleCode = "111-222-333";
                var quantity = 10;
                var maxExpectedPrice = 99;

                mock.Mock<IOrderService>()
                    .Setup(x => x.OrderArticle(articleCode, quantity, maxExpectedPrice))
                    .Returns(new List<OrderItem> { new OrderItem { Id = Guid.NewGuid().ToString(), ArticleCode = articleCode, InventoryId = It.IsAny<string>(), Price = maxExpectedPrice, Quantity = quantity } });

                mock.Mock<ILogger<ShopService>>();

                var shopService = mock.Create<ShopService>();

                // Act
                var actual = shopService.SellArticle(articleCode, quantity, maxExpectedPrice);

                // Assert
                Assert.Equal(actual.First().ArticleCode, articleCode);
                Assert.Equal(actual.Sum(x => x.Quantity), quantity);
            }
        }

        [Fact]
        public void SellArticle_CannotCreateOrderItems()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var articleCode = "111-222-333";
                var quantity = 10;
                var maxExpectedPrice = 99;

                mock.Mock<IOrderService>()
                    .Setup(x => x.OrderArticle(articleCode, quantity, maxExpectedPrice))
                    .Returns(new List<OrderItem> { });

                mock.Mock<ILogger<ShopService>>();

                var shopService = mock.Create<ShopService>();

                // Act
                var actual = shopService.SellArticle(articleCode, quantity, maxExpectedPrice);

                // Assert
                Assert.True(!actual.Any());
            }
        }

        [Fact]
        public void CompleteOrder_HappyFlow()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var orderItems = new List<OrderItem> { new OrderItem { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", InventoryId = "123", Price = 1991, Quantity = 100 } };
                var buyerId = Guid.NewGuid().ToString();

                mock.Mock<IOrderService>()
                    .Setup(x => x.CreateOrder(orderItems, buyerId))
                    .Returns(new Order { Id = Guid.NewGuid().ToString(), Items = orderItems, TotalPrice = orderItems.Sum(x => x.Price), CreatedAt = It.IsAny<DateTime>() });

                mock.Mock<OrderMetrics>();
                mock.Mock<ILogger<ShopService>>();

                var shopService = mock.Create<ShopService>();

                // Act
                shopService.CompleteOrder(orderItems, buyerId);

                // Assert
                Assert.Equal(1, mock.Mock<OrderMetrics>().Object.Completed);
            }
        }

        [Fact]
        public void CompleteOrder_OrderFailed()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var orderItems = new List<OrderItem> { };
                var buyerId = Guid.NewGuid().ToString();

                mock.Mock<OrderMetrics>();
                var orderMetrics = mock.Mock<OrderMetrics>().Object;

                mock.Mock<IOrderService>()
                    .Setup(x => x.CreateOrder(orderItems, buyerId))
                    .Throws(new OrderCreationFailedException("Order creation failed.", orderMetrics));

                mock.Mock<ILogger<ShopService>>();

                var shopService = mock.Create<ShopService>();

                // Act
                shopService.CompleteOrder(orderItems, buyerId);

                // Assert
                Assert.Equal(1, mock.Mock<OrderMetrics>().Object.Failed);
            }
        }
    }
}
