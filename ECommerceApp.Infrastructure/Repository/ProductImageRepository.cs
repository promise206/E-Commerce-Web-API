using ECommerceApp.Core.Interface;
using ECommerceApp.Domain.Model;

namespace ECommerceApp.Infrastructure.Repository
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
