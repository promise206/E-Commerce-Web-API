using ECommerceApp.Core.Interface;

namespace ECommerceApp.Core.Interface
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IProductRepository ProductRepository { get; }
        IRoleRepository RoleRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IUserRepository UserRepository { get; }

        Task Commit();
        Task CreateTransaction();
        void Dispose();
        Task Rollback();
        Task Save();
    }
}