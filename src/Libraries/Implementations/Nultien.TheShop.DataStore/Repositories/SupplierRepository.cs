using Microsoft.Extensions.Logging;

namespace Nultien.TheShop.DataStore.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly InMemoryDbContext context;
        private readonly ILogger<SupplierRepository> logger;

        public SupplierRepository(InMemoryDbContext context, ILogger<SupplierRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
    }
}
