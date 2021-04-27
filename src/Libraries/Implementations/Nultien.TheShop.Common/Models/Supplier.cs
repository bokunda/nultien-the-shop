using System.Collections.Generic;

namespace Nultien.TheShop.Common.Models
{
    public class Supplier
    {
        public Supplier()
        {
            Inventories = new List<Inventory>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public List<Inventory> Inventories { get; set; }
    }
}
