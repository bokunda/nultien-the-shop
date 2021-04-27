using Nultien.TheShop.Common.Models;

namespace Nultien.TheShop.DataStore.Repositories
{
    public interface ISupplierRepository
    {
        Supplier GetById(string id);
        Supplier GetByName(string name);
        bool AddSupplier(Supplier supplier);
    }
}
