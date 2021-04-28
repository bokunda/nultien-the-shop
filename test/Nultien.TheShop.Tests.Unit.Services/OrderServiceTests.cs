using Autofac.Extras.Moq;
using Microsoft.Extensions.Logging;
using Moq;
using Nultien.TheShop.Common.Enums;
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
    public class OrderServiceTests
    {
        [Fact]
        public void CanOrderArticle_HappyFlow()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var articleCode = "111-222-333";
                var articleQuantity = 10;
                var articlePrice = 5;

                mock.Mock<IInventoryRepository>()
                    .Setup(x => x.GetArticleFromInventory(It.IsAny<InventoryIndexType>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Func<Inventory, bool>>()))
                    .Returns(new List<Inventory>() { new Inventory { Id = It.IsAny<string>(), ArticleCode = articleCode, Quantity = articleQuantity, Price = articlePrice } });

                mock.Mock<IOrderRepository>()
                    .Setup(x => x.CreateOrderItem(It.IsAny<List<Inventory>>(), It.IsAny<long>()))
                    .Returns(new List<OrderItem>() { new OrderItem { Id = It.IsAny<string>(), ArticleCode = articleCode, Quantity = articleQuantity, Price = articlePrice, InventoryId = It.IsAny<string>() } });

                mock.Mock<ILogger<OrderService>>();

                var orderService = mock.Create<OrderService>();

                // Act
                var actual = orderService.OrderArticle(articleCode, articleQuantity, articlePrice);

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(actual.First().ArticleCode, articleCode);
                Assert.Equal(actual.Sum(x => x.Price * x.Quantity), articlePrice * articleQuantity);
            }
        }

        [Fact]
        public void CanOrderArticle_NoArticlesInInventoryToSatisfyCondition()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var articleCode = "111-222-333";
                var articleQuantity = 10;
                var articlePrice = 5;

                mock.Mock<IInventoryRepository>()
                    .Setup(x => x.GetArticleFromInventory(It.IsAny<InventoryIndexType>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Func<Inventory, bool>>()))
                    .Returns(new List<Inventory>() { new Inventory { Id = It.IsAny<string>(), ArticleCode = articleCode, Quantity = 1, Price = articlePrice } });

                mock.Mock<IOrderRepository>()
                    .Setup(x => x.CreateOrderItem(It.IsAny<List<Inventory>>(), It.IsAny<long>()))
                    .Returns(new List<OrderItem>() { });

                mock.Mock<ILogger<OrderService>>();

                var orderService = mock.Create<OrderService>();

                // Act
                var actual = orderService.OrderArticle(articleCode, articleQuantity, articlePrice);

                // Assert
                Assert.True(!actual.Any());
            }
        }

        [Fact]
        public void CanOrderArticle_NoOrderItems()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var articleCode = "111-222-333";
                var articleQuantity = 10;
                var articlePrice = 5;

                mock.Mock<IInventoryRepository>()
                    .Setup(x => x.GetArticleFromInventory(It.IsAny<InventoryIndexType>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Func<Inventory, bool>>()))
                    .Returns(new List<Inventory>() { });

                mock.Mock<IOrderRepository>()
                    .Setup(x => x.CreateOrderItem(It.IsAny<List<Inventory>>(), It.IsAny<long>()))
                    .Returns(new List<OrderItem>() { });

                mock.Mock<ILogger<OrderService>>();

                var orderService = mock.Create<OrderService>();

                // Act
                var actual = orderService.OrderArticle(articleCode, articleQuantity, articlePrice);

                // Assert
                Assert.True(!actual.Any());
            }
        }

        [Fact]
        public void CreateOrder_HappyFlow()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var orderItems = new List<OrderItem> { new OrderItem { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", InventoryId = "123", Price = 1991, Quantity = 10 } };
                var buyerId = Guid.NewGuid().ToString();

                mock.Mock<IOrderRepository>()
                    .Setup(x => x.CreateOrder(It.IsAny<List<OrderItem>>(), It.IsAny<string>()))
                    .Returns(new Order { Id = It.IsAny<string>(), Items = It.IsAny<List<OrderItem>>(), TotalPrice = It.IsAny<double>(), CreatedAt = It.IsAny<DateTime>() });

                mock.Mock<ILogger<OrderService>>();

                var orderService = mock.Create<OrderService>();

                // Act
                var actual = orderService.CreateOrder(orderItems, buyerId);

                // Assert
                Assert.NotNull(actual);
            }
        }

        [Fact]
        public void CreateOrder_OrderCreationFailed()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var orderItems = new List<OrderItem> { new OrderItem { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", InventoryId = "123", Price = 1991, Quantity = 10 } };
                var buyerId = Guid.NewGuid().ToString();

                mock.Mock<IOrderRepository>()
                    .Setup(x => x.CreateOrder(It.IsAny<List<OrderItem>>(), It.IsAny<string>()))
                    .Throws(new OrderCreationFailedException("Order creation failed.", new OrderMetrics()));

                mock.Mock<ILogger<OrderService>>();

                var orderService = mock.Create<OrderService>();

                // Act
                // Assert
                Assert.Throws<OrderCreationFailedException>(() => orderService.CreateOrder(orderItems, buyerId));
            }
        }
    }
}
