using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Enum
{
    public enum OrderStatus
    {
        Processing=1,
        InTransit,
        OutForDelivery,
        Delivered,
        Failed
    }
}
