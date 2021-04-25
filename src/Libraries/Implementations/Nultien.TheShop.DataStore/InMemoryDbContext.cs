using Nultien.TheShop.Common.Models;
using System.Collections.Generic;

namespace Nultien.TheShop.DataStore
{
    public class InMemoryDbContext
    {
        public Dictionary<long, Supplier> Suppliers { get; set; }
        /// <summary>
        /// ArticleId, Inventory
        /// </summary>
        public Dictionary<long, Inventory> Inventories { get; set; }
        public Dictionary<long, Article> Articles { get; set; }
        public Dictionary<long, Customer> Customers { get; set; }
        public Dictionary<long, Order> Orders { get; set; }
    }
}
