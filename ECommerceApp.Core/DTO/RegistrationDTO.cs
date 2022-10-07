using ECommerceApp.Core.Utilities;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Core.DTO
{
    public class RegistrationDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Do not enter more than 20 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Do not enter more than 20 characters")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordAgain { get; set; }
        [Required(ErrorMessage ="Enter Address")]
        public string Address { get; set; }
        [Required]
        [StringLength(15, ErrorMessage ="Invalid PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
