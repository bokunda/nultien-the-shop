using Nultien.TheShop.Common.Models;
using Nultien.TheShop.DataStore.Wrappers;
using System.Collections.Generic;

namespace Nultien.TheShop.DataStore
{
    public class InMemoryDbContext
    {
        public InMemoryDbContext()
        {
            Suppliers = new SupplierList();
            Inventories = new InventoryList();
            Articles = new ArticleList();
            Customers = new CustomerList();
            Orders = new OrderList();
        }

        public SupplierList Suppliers { get; set; }
        public InventoryList Inventories { get; set; }
        public ArticleList Articles { get; set; }
        public CustomerList Customers { get; set; }
        public OrderList Orders { get; set; }
    }
}
