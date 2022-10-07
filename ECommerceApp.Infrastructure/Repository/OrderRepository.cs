using ECommerceApp.Core.Interface;
using ECommerceApp.Domain.Model;

namespace ECommerceApp.Infrastructure.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
