using System.Collections.Generic;

namespace Nultien.TheShop.Common.Models
{
    public class Supplier
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}
