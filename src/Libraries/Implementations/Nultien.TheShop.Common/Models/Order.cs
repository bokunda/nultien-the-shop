using System;
using System.Collections.Generic;

namespace Nultien.TheShop.Common.Models
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public string Id { get; set; }
        public List<OrderItem> Items { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
