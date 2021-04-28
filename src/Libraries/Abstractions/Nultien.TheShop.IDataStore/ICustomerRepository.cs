using Nultien.TheShop.Common.Models;

namespace Nultien.TheShop.IDataStore
{
    public interface ICustomerRepository
    {
        bool AssignOrderToCustomer(Order order, string customerId);
    }
}
