using ECommerceApp.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }    

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if(item.Entity is BaseEntity entity)
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            entity.UpdatedAt = DateTimeOffset.UtcNow;
                            break;
                        case EntityState.Added:
                            entity.Id = Guid.NewGuid().ToString();
                            entity.CreatedAt = entity.UpdatedAt = DateTimeOffset.UtcNow;
                            break;
                        default:
                            break;
                    }
                }

                if(item.Entity is UserRole userRoleEntity)
                    if(item.State == EntityState.Added) userRoleEntity.Id = Guid.NewGuid().ToString();
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<OrderDetail> OrderDetails { get; set; }    
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; } 
    }
}
