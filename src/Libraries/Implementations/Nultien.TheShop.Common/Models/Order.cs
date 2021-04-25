using System;

namespace Nultien.TheShop.Common.Models
{
    public class Order
    {
        public long Id { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
