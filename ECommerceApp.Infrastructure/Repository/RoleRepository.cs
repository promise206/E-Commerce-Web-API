using ECommerceApp.Core.Interface;
using ECommerceApp.Domain.Model;

namespace ECommerceApp.Infrastructure.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
