namespace Nultien.TheShop.Common.Models
{
    public class Article
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Unique article identifier (for example: barcode, ISBN, etc.)
        /// </summary>
        public string Code { get; set; }

        public override string ToString()
        {
            return $"Article Id: {Id}, Name: {Name}, Description: {Description}, Code: {Code}.";
        }

    }
}
