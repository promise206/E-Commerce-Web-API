using ECommerceApp.Core.Interface;
using ECommerceApp.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllTransaction()
        {
            try
            {
                return Ok(_transactionService.GetAllTransactionAsync());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Try Again Later Please");
            }
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTransactionsById(string id)
        {
            try
            {
                return Ok(_transactionService.GetTransactionByUserIdAsync(id));
            }
            catch(Exception)
            {
                return StatusCode(500, "Internal Server Error. Try Again Later Please");
            }
        }

        [HttpGet("{pageSize, pageNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTransactionByPagination(int pagesize, int pageNumber)
        {
            try
            {
                return Ok(_transactionService.GetTransactionsByPaginationAsync(pagesize, pageNumber));
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("{transactionid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTransactionsByUserId(string transactionid)
        {
            try
            {
                return Ok(_transactionService.GetTransactionByUserIdAsync(transactionid));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Try Again Later Please");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddTransaction(Transaction transaction)
        {
            try
            {
                _transactionService.AddTransactionAsync(transaction);
                return Ok(GetAllTransaction());
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTransaction(string id)
        {
            try
            {
                _transactionService.DeleteTransactionById(id);
                return Ok(GetAllTransaction());
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
