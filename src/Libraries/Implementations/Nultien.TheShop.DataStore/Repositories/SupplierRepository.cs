using Microsoft.Extensions.Logging;
using Nultien.TheShop.Common.Models;
using System.Linq;

namespace Nultien.TheShop.DataStore.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly InMemoryDbContext context;

        public SupplierRepository(InMemoryDbContext context)
        {
            this.context = context;
        }

        public Supplier GetById(string id)
        {
            return context.Suppliers.FirstOrDefault(x => x.Id.Equals(id));
        }

        public Supplier GetByName(string name)
        {
            return context.Suppliers.FirstOrDefault(x => x.Id.Equals(name));
        }

        public bool AddSupplier(Supplier supplier)
        {
            if (GetByName(supplier.Name) == null)
            {
                context.Suppliers.Add(supplier);
                return true;
            }

            return false;
        }
    }
}
