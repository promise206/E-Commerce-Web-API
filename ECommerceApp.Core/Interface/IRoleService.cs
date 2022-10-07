using ECommerceApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Core.Interface
{
    public interface IRoleService
    {
        Task<IList<string>> GetRolesAsync(User user);
        Task AddToRoleAsync(User user, string roleName);
    }
}
