namespace Nultien.TheShop.Common.Models
{
    public class Inventory
    {
        public long Id { get; set; }
        public Article Article { get; set; } = new Article();
        public long Quantity { get; set; }
        public double Price { get; set; }
    }
}
