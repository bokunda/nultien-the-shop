using Autofac.Extras.Moq;
using Microsoft.Extensions.Logging;
using Moq;
using Nultien.TheShop.Common.Enums;
using Nultien.TheShop.Common.Exceptions;
using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore;
using Nultien.TheShop.DataStore.Repositories;
using Nultien.TheShop.IDataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Nultien.TheShop.Tests.Unit.DataStore
{
    public class OrderRepositoryTests
    {
        [Fact]
        public void OrderRepository_CreateOrderItem()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var inventories = new List<Inventory> { new Inventory { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", Price = 1991, Quantity = 19 } };
                var quantityToOrder = 10;

                mock.Mock<IInventoryRepository>()
                  .Setup(x => x.DecreaseQuantity(It.IsAny<string>(), quantityToOrder))
                  .Returns(true);

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Inventories.AddRange(inventories);

                mock.Mock<ILogger<OrderRepository>>();
                var orderRepository = mock.Create<OrderRepository>();

                // Act
                var actual = orderRepository.CreateOrderItem(inventories, quantityToOrder);

                // Assert
                Assert.Single(actual);
                Assert.Equal(actual.First().Quantity, quantityToOrder);
            }
        }

        [Fact]
        public void OrderRepository_CreateOrderItemNotEnoughQuantity()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var inventories = new List<Inventory> { new Inventory { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", Price = 1991, Quantity = 19 } };
                var quantityToOrder = 10000;

                mock.Mock<IInventoryRepository>()
                  .Setup(x => x.DecreaseQuantity(It.IsAny<string>(), quantityToOrder))
                  .Returns(false);

                mock.Mock<IInventoryRepository>()
                  .Setup(x => x.IncreaseQuantity(It.IsAny<string>(), quantityToOrder))
                  .Returns(true);

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Inventories.AddRange(inventories);

                mock.Mock<ILogger<OrderRepository>>();
                var orderRepository = mock.Create<OrderRepository>();

                // Act
                var actual = orderRepository.CreateOrderItem(inventories, quantityToOrder);

                // Assert
                Assert.True(!actual.Any());
            }
        }

        [Fact]
        public void OrderRepository_AddOrder()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var orderItems = new List<OrderItem> { new OrderItem { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", Price = 1991, Quantity = 19 } };
                var order = new Order { Id = Guid.NewGuid().ToString(), Items = orderItems, CreatedAt = DateTime.UtcNow, TotalPrice = orderItems.Sum(x => x.Price * x.Quantity) };                 

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                mock.Mock<ILogger<OrderRepository>>();
                var orderRepository = mock.Create<OrderRepository>();

                // Act
                orderRepository.Add(order);

                // Assert
                Assert.Single(context.Orders);
            }
        }

        [Fact]
        public void OrderRepository_CreateOrder()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var customer = new Customer { Id = Guid.NewGuid().ToString(), Email = "test@test.test", FirstName = "First", LastName = "Last", Phone = "+123456789", Orders = new List<Order>() };
                var orderItems = new List<OrderItem> { new OrderItem { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", Price = 1991, Quantity = 19 } };
                var order = new Order { Id = Guid.NewGuid().ToString(), Items = orderItems, CreatedAt = DateTime.UtcNow, TotalPrice = orderItems.Sum(x => x.Price * x.Quantity) };


                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                context.Customers.Add(customer);

                mock.Mock<ICustomerRepository>()
                    .Setup(x => x.AssignOrderToCustomer(It.IsAny<Order>(), It.IsAny<string>()))
                    .Callback(() =>
                    {
                        context.Customers.Where(x => x.Id.Equals(customer.Id)).First().Orders.Add(order);
                    })
                    .Returns(true);

                mock.Mock<ILogger<OrderRepository>>();
                var orderRepository = mock.Create<OrderRepository>();

                // Act
                orderRepository.CreateOrder(orderItems, customer.Id);

                // Assert
                Assert.Single(context.Orders);
            }
        }

        [Fact]
        public void OrderRepository_CreateOrderNegative()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var customer = new Customer { Id = Guid.NewGuid().ToString(), Email = "test@test.test", FirstName = "First", LastName = "Last", Phone = "+123456789", Orders = new List<Order>() };
                var orderItems = new List<OrderItem> { new OrderItem { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", Price = 1991, Quantity = 19 } };
                var order = new Order { Id = Guid.NewGuid().ToString(), Items = orderItems, CreatedAt = DateTime.UtcNow, TotalPrice = orderItems.Sum(x => x.Price * x.Quantity) };


                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                context.Customers.Add(customer);

                mock.Mock<ICustomerRepository>()
                    .Setup(x => x.AssignOrderToCustomer(It.IsAny<Order>(), It.IsAny<string>()))
                    .Returns(false);

                mock.Mock<ILogger<OrderRepository>>();
                var orderRepository = mock.Create<OrderRepository>();

                // Act
                // Assert
                Assert.Throws<OrderCreationFailedException>(() => orderRepository.CreateOrder(orderItems, customer.Id));
            }
        }
    }
}
