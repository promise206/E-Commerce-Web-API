using ECommerceApp.Core.DTO;
using ECommerceApp.Domain.Model;

namespace ECommerceApp.Core.Interface
{
    public interface IProductService
    {
        Task<ResponseDTO<bool>> AddProductAsync(AddProductDTO product);
        ResponseDTO<PaginationResult<IEnumerable<ProductDTO>>> GetProductsByPaginationAsync(int pageSize, int pageNumber);
        Task<ResponseDTO<ProductDTO>> GetProductByIdAsync(string id);
    }
}