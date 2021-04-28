using Nultien.TheShop.Common.Models;

namespace Nultien.TheShop.IDataStore
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Assigning given order to the customer
        /// </summary>
        /// <param name="order"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        bool AssignOrderToCustomer(Order order, string customerId);
    }
}
