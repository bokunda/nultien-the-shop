using Autofac.Extras.Moq;
using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Enums;
using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore;
using Nultien.TheShop.DataStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Nultien.TheShop.Tests.Unit.DataStore
{
    public class InventoryRepositoryTests
    {
        [Fact]
        public void CustomerRepository_AssignOrderToCustomer()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var inventory = new Inventory { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", Price = 1991, Quantity = 19 };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Inventories.Add(inventory);

                mock.Mock<ILogger<InventoryRepository>>();
                var inventoryRepository = mock.Create<InventoryRepository>();

                // Act
                var actualCode = inventoryRepository.GetArticleFromInventory(InventoryIndexType.ArticleCode, string.Empty, inventory.ArticleCode, x => x.Price <= inventory.Price);
                var actualId = inventoryRepository.GetArticleFromInventory(InventoryIndexType.InventoryId, inventory.Id, string.Empty, x => x.Price <= inventory.Price);

                // Assert
                Assert.Single(actualCode);
                Assert.Single(actualId);
            }
        }

        [Fact]
        public void CustomerRepository_AssignOrderToCustomerInvalidIndex()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var inventory = new Inventory { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", Price = 1991, Quantity = 19 };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Inventories.Add(inventory);

                mock.Mock<ILogger<InventoryRepository>>();
                var inventoryRepository = mock.Create<InventoryRepository>();

                // Act
                var actualCode = inventoryRepository.GetArticleFromInventory(InventoryIndexType.ArticleCode, string.Empty, "INVALID", x => x.Price <= inventory.Price);
                var actualId = inventoryRepository.GetArticleFromInventory(InventoryIndexType.InventoryId, "INVALID", string.Empty, x => x.Price <= inventory.Price);

                // Assert
                Assert.True(!actualCode.Any());
                Assert.True(!actualId.Any());
            }
        }

        [Fact]
        public void CustomerRepository_AssignOrderToCustomerInvalidPrice()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var inventory = new Inventory { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", Price = 1991, Quantity = 19 };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Inventories.Add(inventory);

                mock.Mock<ILogger<InventoryRepository>>();
                var inventoryRepository = mock.Create<InventoryRepository>();

                // Act
                var actualCode = inventoryRepository.GetArticleFromInventory(InventoryIndexType.ArticleCode, string.Empty, inventory.ArticleCode, x => x.Price <= inventory.Price - 100);
                var actualId = inventoryRepository.GetArticleFromInventory(InventoryIndexType.InventoryId, inventory.Id, string.Empty, x => x.Price <= inventory.Price - 100);

                // Assert
                Assert.True(!actualCode.Any());
                Assert.True(!actualId.Any());
            }
        }

        [Fact]
        public void CustomerRepository_DecreaseQuantity()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var inventory = new Inventory { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", Price = 1991, Quantity = 19 };
                var decrement = 10;

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Inventories.Add(inventory);

                mock.Mock<ILogger<InventoryRepository>>();
                var inventoryRepository = mock.Create<InventoryRepository>();

                // Act
                var actual = inventoryRepository.DecreaseQuantity(inventory.Id, decrement);

                // Assert
                Assert.True(actual);
                Assert.Equal(context.Inventories.First().Quantity, inventory.Quantity);
            }
        }

        [Fact]
        public void CustomerRepository_DecreaseQuantityNotEnoughQuantity()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var inventory = new Inventory { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", Price = 1991, Quantity = 19 };
                var decrement = 1000;

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Inventories.Add(inventory);

                mock.Mock<ILogger<InventoryRepository>>();
                var inventoryRepository = mock.Create<InventoryRepository>();

                // Act
                var actual = inventoryRepository.DecreaseQuantity(inventory.Id, decrement);

                // Assert
                Assert.True(!actual);
                Assert.Equal(context.Inventories.First().Quantity, inventory.Quantity);
            }
        }

        [Fact]
        public void CustomerRepository_DecreaseQuantityInventoryDoesntExist()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var decrement = 1000;

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                mock.Mock<ILogger<InventoryRepository>>();
                var inventoryRepository = mock.Create<InventoryRepository>();

                // Act
                var actual = inventoryRepository.DecreaseQuantity("INVALID", decrement);

                // Assert
                Assert.True(!actual);
            }
        }

        [Fact]
        public void CustomerRepository_IncreaseQuantity()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var inventory = new Inventory { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", Price = 1991, Quantity = 19 };
                var increment = 10;

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Inventories.Add(inventory);

                mock.Mock<ILogger<InventoryRepository>>();
                var inventoryRepository = mock.Create<InventoryRepository>();

                // Act
                var actual = inventoryRepository.IncreaseQuantity(inventory.Id, increment);

                // Assert
                Assert.True(actual);
                Assert.Equal(context.Inventories.First().Quantity, inventory.Quantity);
            }
        }

        [Fact]
        public void CustomerRepository_IncreaseQuantityInventoryDoesntExist()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var increment = 1000;

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                mock.Mock<ILogger<InventoryRepository>>();
                var inventoryRepository = mock.Create<InventoryRepository>();

                // Act
                var actual = inventoryRepository.DecreaseQuantity("INVALID", increment);

                // Assert
                Assert.True(!actual);
            }
        }
    }
}
