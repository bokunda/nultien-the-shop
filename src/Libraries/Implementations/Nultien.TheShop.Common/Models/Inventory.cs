namespace Nultien.TheShop.Common.Models
{
    public class Inventory
    {
        public string Id { get; set; }
        public string ArticleCode { get; set; }
        public long Quantity { get; set; }
        public double Price { get; set; }
    }
}
