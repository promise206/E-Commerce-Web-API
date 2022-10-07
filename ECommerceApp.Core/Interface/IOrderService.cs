using ECommerceApp.Core.DTO;
using ECommerceApp.Domain.Enum;
using ECommerceApp.Domain.Model;

namespace ECommerceApp.Core.Interface
{
    public interface IOrderService
    {
        Task<ResponseDTO<string>> AddOrderAsync(List<OrderDetailsDTO> orderDetails, string userId);
        Task<ResponseDTO<string>> DeleteOrderById(string orderId);
        Task<ResponseDTO<string>> DeleteOrderDetailsAsync(string orderDetailsId);
        //IQueryable<Order> GetOrdersByStatus(string UserId, OrderStatus orderStatus);
        //IQueryable<Order> GetOrdersByUserIdAsync(string UserId);
        ResponseDTO<PaginationResult<IEnumerable<Order>>> GetOrdersByStatusAndPagination(string UserId, OrderStatus orderStatus, int pageSize, int pageNumber);
        ResponseDTO<PaginationResult<IEnumerable<Order>>> GetOrdersByPagination(string UserId, int pageSize, int pageNumber);
    }
}