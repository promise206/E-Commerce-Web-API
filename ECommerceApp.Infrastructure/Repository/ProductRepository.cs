using ECommerceApp.Core.Interface;
using ECommerceApp.Domain.Model;

namespace ECommerceApp.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
