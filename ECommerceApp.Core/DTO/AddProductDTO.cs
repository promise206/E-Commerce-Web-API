using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Core.DTO
{
    public class AddProductDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        public string Name { get; set; }
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
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool IsDiscountAvailable { get; set; }
    }
}
