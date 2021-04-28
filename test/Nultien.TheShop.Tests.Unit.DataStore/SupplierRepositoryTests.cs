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
    public class SupplierRepositoryTests
    {
        [Fact]
        public void SupplierRepositoryTests_GetByName()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var supplier = new Supplier { Id = Guid.NewGuid().ToString(), Name = "Supplier1", Inventories = new List<Inventory>() };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Suppliers.Add(supplier);

                mock.Mock<ILogger<SupplierRepository>>();
                var supplierRepository = mock.Create<SupplierRepository>();

                // Act
                var actual = supplierRepository.GetByName(supplier.Name);

                // Assert
                Assert.Equal(context.Suppliers.First().Name, actual.Name);
            }
        }

        [Fact]
        public void SupplierRepositoryTests_GetByNameNegative()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var supplier = new Supplier { Id = Guid.NewGuid().ToString(), Name = "Supplier1", Inventories = new List<Inventory>() };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;
                context.Suppliers.Add(supplier);

                mock.Mock<ILogger<SupplierRepository>>();
                var supplierRepository = mock.Create<SupplierRepository>();

                // Act
                var actual = supplierRepository.GetByName("INVALID");

                // Assert
                Assert.Null(actual);
            }
        }

        [Fact]
        public void SupplierRepositoryTests_AddSupplier()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var supplier = new Supplier { Id = Guid.NewGuid().ToString(), Name = "Supplier1", Inventories = new List<Inventory>() };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                mock.Mock<ILogger<SupplierRepository>>();
                var supplierRepository = mock.Create<SupplierRepository>();

                // Act
                var actual = supplierRepository.AddSupplier(supplier);

                // Assert
                Assert.True(actual);
                Assert.Equal(context.Suppliers.First().Name, supplier.Name);
            }
        }

        [Fact]
        public void SupplierRepositoryTests_AddSupplierNegative()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var supplier = new Supplier { Id = Guid.NewGuid().ToString(), Name = "Supplier1", Inventories = new List<Inventory>() };
                var supplier2 = new Supplier { Id = Guid.NewGuid().ToString(), Name = "Supplier1", Inventories = new List<Inventory>() };

                mock.Mock<InMemoryDbContext>();
                var context = mock.Mock<InMemoryDbContext>().Object;

                mock.Mock<ILogger<SupplierRepository>>();
                var supplierRepository = mock.Create<SupplierRepository>();

                // Act
                var actual = supplierRepository.AddSupplier(supplier);
                var actual2 = supplierRepository.AddSupplier(supplier2);

                // Assert
                Assert.True(!actual2);
            }
        }
    }
}
