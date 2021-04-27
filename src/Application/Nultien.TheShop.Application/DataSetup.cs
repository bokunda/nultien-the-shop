using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nultien.TheShop.Application
{
    public static class DataSetup
    {
        public static void InsertData(InMemoryDbContext context)
        {
            context.Customers.AddRange(GetCustomers());
            context.Articles.AddRange(GetArticles());
            context.Suppliers.AddRange(GetSuppliers(3));

            foreach (var supplier in context.Suppliers)
            {
                context.Inventories.AddRange(supplier.Inventories);
            }
        }

        private static List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = Guid.NewGuid().ToString(), Email = "customer01@dummycust.xyz", FirstName = "Fname1", LastName = "Lname1", Phone = "+123456789", Orders = new List<Order>()},
                new Customer { Id = Guid.NewGuid().ToString(), Email = "customer02@dummycust.xyz", FirstName = "Fname2", LastName = "Lname2", Phone = "+112233445", Orders = new List<Order>()},
                new Customer { Id = Guid.NewGuid().ToString(), Email = "customer03@dummycust.xyz", FirstName = "Fname3", LastName = "Lname3", Phone = "+111222333", Orders = new List<Order>()},
            };
        }

        private static List<Article> GetArticles()
        {
            return new List<Article>
            {
                new Article { Id = Guid.NewGuid().ToString(), Code = "123-456-789", Name = "Article 01", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing." },
                new Article { Id = Guid.NewGuid().ToString(), Code = "111-222-333", Name = "Article 02", Description = "Sed do eiusmod tempor incididun." },
                new Article { Id = Guid.NewGuid().ToString(), Code = "222-333-444", Name = "Article 03", Description = "Ullamco laboris nisi ut aliquip ex ea commodo." },
                new Article { Id = Guid.NewGuid().ToString(), Code = "333-444-555", Name = "Article 04", Description = "Ut enim ad minima veniam." },
                new Article { Id = Guid.NewGuid().ToString(), Code = "444-555-666", Name = "Article 05", Description = "At vero eos et accusamus et iusto odio." },
                new Article { Id = Guid.NewGuid().ToString(), Code = "555-666-777", Name = "Article 06", Description = "Doloribus asperiores repellat.." },
                new Article { Id = Guid.NewGuid().ToString(), Code = "666-777-888", Name = "Article 07", Description = "Et harum quidem rerum facilis." },
                new Article { Id = Guid.NewGuid().ToString(), Code = "777-888-999", Name = "Article 08", Description = "Temporibus autem quibusdam et aut officiis debitis." },
                new Article { Id = Guid.NewGuid().ToString(), Code = "888-999-000", Name = "Article 09", Description = "Excepteur sint occaecat cupidatat non proident." },
            };
        }

        private static List<Inventory> GetInventories(long basePrice, long baseQuantity)
        {
            var articles = GetArticles();
            var inventories = new List<Inventory>();

            foreach (var article in articles)
            {
                inventories.Add(new Inventory { Id = Guid.NewGuid().ToString(), ArticleCode = article.Code, Price = baseQuantity * basePrice / 2 + 41, Quantity = baseQuantity++ });
            }

            return inventories;
        }

        private static List<Supplier> GetSuppliers(long totalSuppliers)
        {
            var suppliers = new List<Supplier>();

            while(totalSuppliers > 0)
            {
                var inventories = GetInventories(totalSuppliers * 2, totalSuppliers * 100);
                suppliers.Add(new Supplier { Id = Guid.NewGuid().ToString(), Name = $"Supplier {totalSuppliers}", Inventories = inventories });
                totalSuppliers--;
            }

            return suppliers;
        }
    }
}
