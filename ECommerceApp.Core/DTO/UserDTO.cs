using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Core.DTO
{
    class UserDTO
    {
        [Required]
        [StringLength(20, ErrorMessage = "Do not enter more than 20 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Do not enter more than 20 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter Email"), MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password is not valid.")]
        public string Password { get; set; }       
    }
}
