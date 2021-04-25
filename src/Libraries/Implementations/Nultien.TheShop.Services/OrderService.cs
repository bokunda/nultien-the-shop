using Microsoft.Extensions.Logging;
using Nultien.TheShop.DataStore.Repositories;
using System;

namespace Nultien.TheShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> logger;
        private readonly ISupplierRepository supplierRepository;
        private readonly IInventoryRepository inventoryRepository;
        private readonly IArticleRepository articleRepository;

        public OrderService(ISupplierRepository supplierRepository, 
            IInventoryRepository inventoryRepository,
            IArticleRepository articleRepository, 
            ILogger<OrderService> logger)
        {
            this.supplierRepository = supplierRepository;
            this.articleRepository = articleRepository;
            this.inventoryRepository = inventoryRepository;
            this.logger = logger;
        }

        public void OrderArticle(string articleCode)
        {
            throw new NotImplementedException();
        }
    }
}
