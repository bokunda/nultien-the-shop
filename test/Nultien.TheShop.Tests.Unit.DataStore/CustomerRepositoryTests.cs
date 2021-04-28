using Autofac.Extras.Moq;
using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore;
using Nultien.TheShop.DataStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Nultien.TheShop.Tests.Unit.DataStore
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void CustomerRepository_AssignOrderToCustomer()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var orderItems = new List<OrderItem> { new OrderItem { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", InventoryId = "111", Price = 1991, Quantity = 19 } };
                var order = new Order { Id = Guid.NewGuid().ToString(), Items = orderItems, CreatedAt = DateTime.UtcNow, TotalPrice = orderItems.Sum(x => x.Price * x.Quantity) };
                var customer = new Customer { Id = Guid.NewGuid().ToString(), Email = "test@test.test", FirstName = "First", LastName = "Last", Phone = "+123456789", Orders = new List<Order>() };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Customers.Add(customer);

                mock.Mock<ILogger<CustomerRepository>>();

                var customerRepository = mock.Create<CustomerRepository>();

                // Act
                var actual = customerRepository.AssignOrderToCustomer(order, customer.Id);

                // Assert
                Assert.True(actual);
            }
        }

        [Fact]
        public void CustomerRepository_AssignOrderToCustomerUserDoesntExists()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var orderItems = new List<OrderItem> { new OrderItem { Id = Guid.NewGuid().ToString(), ArticleCode = "111-222-333", InventoryId = "111", Price = 1991, Quantity = 19 } };
                var order = new Order { Id = Guid.NewGuid().ToString(), Items = orderItems, CreatedAt = DateTime.UtcNow, TotalPrice = orderItems.Sum(x => x.Price * x.Quantity) };
                var customer = new Customer { Id = Guid.NewGuid().ToString(), Email = "test@test.test", FirstName = "First", LastName = "Last", Phone = "+123456789", Orders = new List<Order>() };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Customers.Add(customer);

                mock.Mock<ILogger<CustomerRepository>>();

                var customerRepository = mock.Create<CustomerRepository>();

                // Act
                var actual = customerRepository.AssignOrderToCustomer(order, "INVALID");

                // Assert
                Assert.True(!actual);
            }
        }
    }
}
