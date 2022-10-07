using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Core.DTO
{
    public class OrderDetailsDTO
    {
        [Required]
        [DataType(DataType.Text)]
        public int Status { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public decimal price { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public int Quantity { get; set; }
        [Required]
        public string ProductId { get; set; }
    }
}
