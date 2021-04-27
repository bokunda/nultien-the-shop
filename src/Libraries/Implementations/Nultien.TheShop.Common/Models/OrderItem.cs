using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nultien.TheShop.Common.Models
{
    public class OrderItem
    {
        public string Id { get; set; }
        public string ArticleCode { get; set; }
        public double Price { get; set; }
        public long Quantity { get; set; }
        public string InventoryId { get; set; }
    }
}
