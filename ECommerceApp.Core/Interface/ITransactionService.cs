using ECommerceApp.Core.DTO;
using ECommerceApp.Domain.Model;

namespace ECommerceApp.Core.Interface
{
    public interface ITransactionService
    {
        bool AddTransactionAsync(Transaction transactions);
        bool DeleteTransactionById(string transactionId);
        IQueryable<Transaction> GetAllTransactionAsync();
        Task<Transaction> GetTransactionByIdAsync(string id);
        Task<Transaction> GetTransactionByUserIdAsync(string transactionid);
        ResponseDTO<PaginationResult<IEnumerable<TransactionDTO>>> GetTransactionsByPaginationAsync(int pageSize, int pageNumber);
    }
}