using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain.Model
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }
        public bool IsDiscountAvailable { get; set; }
        public int Quantity { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
       
        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
