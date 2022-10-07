using ECommerceApp.Core.Interface;
using ECommerceApp.Domain.Model;
using ECommerceApp.Core.Utilities;
using AutoMapper;
using ECommerceApp.Core.DTO;
using System.Net;

namespace ECommerceApp.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public ResponseDTO<PaginationResult<IEnumerable<ProductDTO>>> GetProductsByPaginationAsync(int pageSize, int pageNumber)
        {
            var responseDto = new ResponseDTO<PaginationResult<IEnumerable<ProductDTO>>>();
            try
            {
                var exec = _unitOfWork.ProductRepository.GetAllAsync();
                var response = Paginator.PaginationAsync<Product,ProductDTO>(exec,pageSize,pageNumber,_mapper);
                responseDto.Data = response;
                responseDto.StatusCode = response.PageItems != null ? (int)HttpStatusCode.Accepted : (int)HttpStatusCode.NoContent;
                responseDto.Status = true;
                responseDto.Message = response.PageItems != null ? "Resquest is Successfull" : "the database is Empty";
                return responseDto;
            }
            catch (Exception Ex)
            {
                responseDto.Data =null;
                responseDto.StatusCode = (int)HttpStatusCode.BadRequest;
                responseDto.Status = false;
                responseDto.Message = "Resquest was unSuccessfull";
                responseDto.Error = new List<ErrorItem>();
                responseDto.Error.Add(new ErrorItem() {InnerException = Ex.Message });
                return responseDto;
            }
        }
        public async Task<ResponseDTO<ProductDTO>> GetProductByIdAsync(string id)
        {
            var responseDto = new ResponseDTO<ProductDTO>();
            try
            {
                var exec = _unitOfWork.ProductRepository.GetAllAsync();
                var response = await _unitOfWork.ProductRepository.GetAsync(product => product.Id.Equals(id));
                var category = await _unitOfWork.CategoryRepository.GetAsync(category=>category.Id == response.CategoryId);
                var images = _unitOfWork.ProductImageRepository.GetAllAsync().Where(image => image.ProductId==id).Select(x=>x.ImageUrl).AsEnumerable();
                var productDto = _mapper.Map<ProductDTO>(response);
                productDto.Category = category.Name;
                productDto.ProductImage = images;
                responseDto.Data = productDto;
                responseDto.StatusCode = response != null ? (int)HttpStatusCode.Accepted : (int)HttpStatusCode.NoContent;
                responseDto.Status = response != null ? true : false;
                responseDto.Message = response != null ? "Resquest is Successfull" : "Instance doesn't exist in the Entity";
                return responseDto;
            }
            catch (Exception Ex)
            {
                //throw Ex.Message;
                responseDto.Data = null;
                responseDto.StatusCode = (int)HttpStatusCode.BadRequest;
                responseDto.Status = false;
                responseDto.Message = "Resquest was unSuccessfull";
                responseDto.Error = new List<ErrorItem>();
                responseDto.Error.Add(new ErrorItem() { InnerException = Ex.Message });
                return responseDto;
            }  
        }
        public async Task<ResponseDTO<bool>> AddProductAsync(AddProductDTO product)
        {
            var response = new ResponseDTO<bool>();
            try
            {
                var productEntity = _mapper.Map<Product>(product);
                await _unitOfWork.ProductRepository.InsertAsync(productEntity);
                await _unitOfWork.Save();
                response.Status = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Data = true;
                response.Message = "the insatance was successfully";
                return response;
            }
            catch (Exception Ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Data = false;
                response.Message = "the instance was not created";
                response.Error = new List<ErrorItem>();
                response.Error.Add(new ErrorItem() { InnerException = Ex.Message });
                return response;
            }
        }
    }
}
