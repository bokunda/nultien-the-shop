using Nultien.TheShop.Common.Models;

namespace Nultien.TheShop.DataStore.Repositories
{
    public interface ISupplierRepository
    {
        /// <summary>
        /// Get supplier from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Supplier GetById(string id);

        /// <summary>
        /// Get supplier from the database by supplier name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Supplier GetByName(string name);

        /// <summary>
        /// Add supplier in the database.
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        bool AddSupplier(Supplier supplier);
    }
}
