using Microsoft.Extensions.Logging;

namespace Nultien.TheShop.DataStore
{
    public class InMemoryDataStore
    {
        private readonly InMemoryDbContext context;
        private readonly ILogger<InMemoryDataStore> logger;

        public InMemoryDataStore(ILogger<InMemoryDataStore> logger, InMemoryDbContext context)
        {
            this.context = context;
            this.logger = logger;
        }
    }
}
