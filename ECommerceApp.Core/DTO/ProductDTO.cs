using ECommerceApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Core.DTO
{
    public class ProductDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Category { get; set; }
        [Required]
        [MaxLength(50)]
        public IEnumerable<string> ProductImage { get; set; }
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public decimal Price { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public decimal Discount { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Please enter correct value")]
        public int Rating { get; set; }
        
    }
}
