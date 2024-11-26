using WebUI.Models.Common;

namespace WebUI.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public DateTime Expires { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
