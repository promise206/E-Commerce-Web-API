using ECommerceApp.Core.Interface;
using ECommerceApp.Domain.Model;

namespace ECommerceApp.Infrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ECommerceDbContext context) : base(context)
        {

        }
    }
}
