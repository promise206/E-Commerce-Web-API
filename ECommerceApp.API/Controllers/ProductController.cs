using AutoMapper;
using ECommerceApp.Core.DTO;
using ECommerceApp.Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetProducts([FromQuery] PaginationDetailsDTO details)
        {
            var response = _service.GetProductsByPaginationAsync(details.PageSize, details.PageNumber);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var response = await _service.GetProductByIdAsync(id);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(AddProductDTO productDto)
        {
            var response = await _service.AddProductAsync(productDto);
            return Ok(response);
        }

    }
}
