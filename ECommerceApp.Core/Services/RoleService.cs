using ECommerceApp.Core.Interface;
using ECommerceApp.Domain.Model;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ECommerceApp.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly string _con;
        public RoleService(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task AddToRoleAsync(User user, string roleName)
        {
            using (var connection = new SqlConnection(_con))
            {
                var Name = roleName;
                var roleId = Convert.ToString(await connection.ExecuteScalarAsync<int?>($"SELECT [Id] FROM [Roles] WHERE [Name] = @{nameof(Name)}", new { Name }));
                if (roleId != null)
                    roleId = Convert.ToString(await connection.ExecuteAsync($"INSERT INTO [Roles] ([Name], [Name]) VALUES(@{nameof(roleName)}, @{nameof(Name)})",
                        new { roleName, Name }));

                await connection.ExecuteAsync($"IF NOT EXISTS(SELECT 1 FROM [UserRoles] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}) " +
                    $"INSERT INTO [UserRoles]([UserId], [RoleId]) VALUES(@userId, @{nameof(roleId)})",
                    new { userId = user.Id, roleId });
            }
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            using (var connection = new SqlConnection(_con))
            {
                var queryResults = await connection.QueryAsync<string>("SELECT r.[Name] FROM [Roles] r INNER JOIN [UserRoles] ur ON ur.[RoleId] = r.Id " +
                    "WHERE ur.UserId = @userId", new { userId = user.Id });
                return queryResults.ToList();
            }
        }
    }
}

/*        public async Task AddToRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetRolesAsync(User user)
        {
            List<UserRole> userRoleList = new List<UserRole>();
            var allUsersRoles = _unitOfWork.RoleRepository.GetAllAsync();
            var userRoles = allUsersRoles.Where(userId => userId.Id.Equals(user.Id)).ToList();
            return userRoles;
        }*/