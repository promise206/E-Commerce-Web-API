using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain.Model
{
    public class Category : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }        
        public ICollection<Product> Products { get; set; }
    }
}
