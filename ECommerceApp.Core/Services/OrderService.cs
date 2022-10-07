using AutoMapper;
using ECommerceApp.Core.DTO;
using ECommerceApp.Core.Interface;
using ECommerceApp.Core.Utilities;
using ECommerceApp.Domain.Enum;
using ECommerceApp.Domain.Model;
using System.Net;

namespace ECommerceApp.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<string>> AddOrderAsync(List<OrderDetailsDTO> orderDetails, string userId)
        {
            var result = new ResponseDTO<string>();
            try
            {
                List<OrderDetail> orderDetailsList = new List<OrderDetail>();
                foreach (var orderDetail in orderDetails)
                {
                    var orderDetailsModel = _mapper.Map<OrderDetail>(orderDetail);
                    orderDetailsList.Add(orderDetailsModel);
                }
                Order orders = new Order()
                {
                    UserId = userId,
                    OrderDetails = orderDetailsList
                };
                await _unitOfWork.OrderRepository.InsertAsync(orders);
                await _unitOfWork.Save();

                result.StatusCode = (int)HttpStatusCode.Created;
                result.Status = true;
                result.Data = string.Empty;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDTO<string>> DeleteOrderDetailsAsync(string orderDetailsId)
        {
            var result = new ResponseDTO<string>();
            try
            {
                if(orderDetailsId == null)
                {
                    result.StatusCode = (int)HttpStatusCode.NotFound;
                    result.Status = true;
                    result.Data = string.Empty;
                    result.Message = "Order Not Found";
                }
                else
                {
                    await _unitOfWork.OrderRepository.DeleteAsync(orderDetailsId);
                    await _unitOfWork.Save();
                    result.StatusCode = (int)HttpStatusCode.OK;
                    result.Status = true;
                    result.Data = string.Empty;
                    result.Message = "Deleted Successfully";
                }
                
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDTO<string>> DeleteOrderById(string orderId)
        {
            var result = new ResponseDTO<string>();
            try
            {
                if (orderId == null)
                {
                    result.StatusCode = (int)HttpStatusCode.NotFound;
                    result.Status = true;
                    result.Data = string.Empty;
                    result.Message = "Order Not Found";
                }
                else
                {
                    await _unitOfWork.OrderRepository.DeleteAsync(orderId);
                    await _unitOfWork.Save();
                    result.StatusCode = (int)HttpStatusCode.OK;
                    result.Status = true;
                    result.Message = "Deleted Successfully";
                    result.Data = string.Empty;

                }               
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        private IQueryable<Order> GetOrdersByUserIdAsync(string UserId)
        {
            return _unitOfWork.OrderRepository.GetAllAsync().Where(order => order.UserId.Equals(UserId));
        }

        public ResponseDTO<PaginationResult<IEnumerable<Order>>> GetOrdersByPagination(string UserId, int pageSize, int pageNumber)
        {
            try
            {
                var response = new ResponseDTO<PaginationResult<IEnumerable<Order>>>();
                var exec = this.GetOrdersByUserIdAsync(UserId);
                var pagination = Paginator.PaginationAsync<Order, Order>(exec, pageSize, pageNumber, _mapper);

                response.Data = pagination;
                response.StatusCode = (int)HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IQueryable<Order> GetOrdersByStatus(string UserId, OrderStatus orderStatus)
        {
            return _unitOfWork.OrderRepository.GetAllAsync().Where(
                        order => order.UserId.Equals(UserId) && ((OrderStatus)order.Status)
                        .Equals(orderStatus));
        }

        public ResponseDTO<PaginationResult<IEnumerable<Order>>> GetOrdersByStatusAndPagination(string UserId, OrderStatus orderStatus, int pageSize, int pageNumber)
        {
            try
            {
                var response = new ResponseDTO<PaginationResult<IEnumerable<Order>>>();
                var exec = GetOrdersByStatus(UserId, orderStatus);
                //var orderDetails = _unitOfWork.OrderDetailsRepository.GetAllAsync().Where(OrderDetail
                //  => OrderDetail.OrderId.Equals(exec.Select(o => o.Id)));
               
                var pagination = Paginator.PaginationAsync<Order, Order>(exec, pageNumber,pageSize, _mapper);
                response.Data = pagination;
                response.StatusCode = (int)HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                throw;
            }            
        }


    }
}
