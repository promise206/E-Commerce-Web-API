using ECommerceApp.Core.DTO;
using ECommerceApp.Core.Interface;
using ECommerceApp.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddOrderAsync(List<OrderDetailsDTO> orderDetails, string userId)
        {
            return Ok(await _service.AddOrderAsync(orderDetails, userId));
        }
        [HttpPost("deleteorderdetail/{orderDetailsId}")]
        public IActionResult DeleteOrderDetailsAsync(string orderDetailsId)
        {
            return Ok(_service.DeleteOrderDetailsAsync(orderDetailsId));
        }

        [HttpPost("deleteorder/{orderId}")]
        public IActionResult DeleteOrderById(string orderId)
        {
            return Ok(_service.DeleteOrderById(orderId));
        }

        [HttpGet("{UserId}")]
        public IActionResult GetOrdersByPagination(string UserId, int pageSize, int pageNumber)
        {
            return Ok(_service.GetOrdersByPagination(UserId, pageSize, pageNumber));
        }

        [HttpGet("status/{UserId}")]
        public IActionResult GetOrdersByStatusAndPagination(string UserId, OrderStatus orderStatus, int pageSize, int pageNumber)
        {
            return Ok(_service.GetOrdersByStatusAndPagination(UserId, orderStatus, pageSize, pageNumber));
        }
    }
}
