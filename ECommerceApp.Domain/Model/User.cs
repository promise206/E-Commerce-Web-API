using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain.Model
{
    public class User : BaseEntity
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
