using ECommerceApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Core.DTO
{
    public class TransactionDTO
    {
        
        public User User;
        public Order Order;
    }
}
