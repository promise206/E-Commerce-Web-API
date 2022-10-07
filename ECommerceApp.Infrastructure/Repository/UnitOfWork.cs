using ECommerceApp.Core.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerceApp.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposedValue;
        private readonly ECommerceDbContext _context;
        private IDbContextTransaction _objTransaction;
        private IUserRepository _userRepository;
        private IProductRepository _productRepository;
        private ITransactionRepository _transactionRepository;
        private ICategoryRepository _categoryRepository;
        private IOrderRepository _orderRepository;
        private IOrderDetailsRepository _orderDetailsRepository;
        private IProductImageRepository _productImageRepository;
        private IRoleRepository _roleRepository;

        public UnitOfWork(ECommerceDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public ITransactionRepository TransactionRepository => _transactionRepository ??= new TransactionRepository(_context);
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);
        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context);
        public IOrderDetailsRepository OrderDetailsRepository => _orderDetailsRepository ??= new OrderDetailsRepository(_context);
        public IProductImageRepository ProductImageRepository => _productImageRepository ??= new ProductImageRepository(_context);
        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_context);


        public async Task CreateTransaction()
        {
            _objTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _objTransaction.CommitAsync();
        }


        public async Task Rollback()
        {
            await _objTransaction?.RollbackAsync();
            await _objTransaction.DisposeAsync();
        }

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
