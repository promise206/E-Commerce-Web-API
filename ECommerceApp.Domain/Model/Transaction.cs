using ECommerceApp.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ECommerceApp.Domain.Model
{
    public class Transaction : BaseEntity
    {
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
