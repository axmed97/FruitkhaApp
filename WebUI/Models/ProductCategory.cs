using System.ComponentModel.DataAnnotations.Schema;
using WebUI.Models.Common;

namespace WebUI.Models
{
    public class ProductCategory : BaseEntity
    {
        public int CategoryId { get; set; }
        // Navigation
        public Category Category { get; set; }
        public int ProductId { get; set; }
        // Navigation
        public Product Product { get; set; }
        [NotMapped]
        public override DateTime CreatedDate { get => base.CreatedDate; set => base.CreatedDate = value; }
        [NotMapped]
        public override DateTime? UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }
    }
}
