using Nultien.TheShop.Common.Models;

namespace Nultien.TheShop.DataStore.Repositories
{
    public interface IOrderRepository
    {
        Order CreateOrder(Inventory inventory, long buyerId);
        void Add(Order order);
    }
}
