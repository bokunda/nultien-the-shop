namespace Nultien.TheShop.Common.Models
{
    public class Article
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Unique article identifier (for example: barcode, ISBN, etc.)
        /// </summary>
        public string Code { get; set; }

    }
}
