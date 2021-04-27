using Nultien.TheShop.Common.Models;
using Nultien.TheShop.IDataStore;
using System.Linq;

namespace Nultien.TheShop.DataStore.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InMemoryDbContext context;

        public CustomerRepository(InMemoryDbContext context)
        {
            this.context = context;
        }

        public void AssignOrderToCustomer(Order order, string customerId)
        {
            var customer = context.Customers.FirstOrDefault(x => x.Id.Equals(customerId));
            customer.Orders.Add(order);
        }
    }
}
