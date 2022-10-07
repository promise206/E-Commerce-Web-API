using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Enum
{
    public enum CategoryEnum
    {
        [Display(Name = "533a7391-bd03-4d5b-bff0-1704773fbfd0")]
        Bags = 1,
        [Display(Name = "235a715d-9b66-4689-ae2b-cc7277dde29e")]
        Sneaker,
        [Display(Name = "0c4075de-bd56-485c-b828-3b1ab011ca63")]
        Belt,
    }
}
