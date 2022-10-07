using AutoMapper;
using ECommerceApp.Core.DTO;
using ECommerceApp.Core.Interface;
using ECommerceApp.Core.Utilities;
using ECommerceApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Core.Services
{
    public class TransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IQueryable<Transaction> GetAllTransactionAsync()
        {
            return _unitOfWork.TransactionRepository.GetAllAsync();
        }

        public Task<Transaction> GetTransactionByIdAsync(string id)
        {
            return _unitOfWork.TransactionRepository.GetAsync(transaction => transaction.Id == id);
        }

        public Task<Transaction> GetTransactionByUserIdAsync(string transactionid)
        {
            return _unitOfWork.TransactionRepository.GetAsync(transaction => transaction.UserId == transactionid);
        }

        public bool AddTransactionAsync(Transaction transactions)
        {
            try
            {
                _unitOfWork.TransactionRepository.InsertAsync(transactions);
                _unitOfWork.Save();
                return true;
            }
            catch(Exception)
            {
                throw;
            }

        }

        public bool DeleteTransactionById(string transactionId)
        {
            try
            {
                _unitOfWork.TransactionRepository.DeleteAsync(transactionId);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseDTO<PaginationResult<IEnumerable<TransactionDTO>>> GetTransactionsByPaginationAsync(int pageSize, int pageNumber)
        {
            var responseDto = new ResponseDTO<PaginationResult<IEnumerable<TransactionDTO>>>();
            try
            {
                var exec = _unitOfWork.TransactionRepository.GetAllAsync();
                var response = Paginator.PaginationAsync<Transaction, TransactionDTO>(exec, pageSize, pageNumber, _mapper);
                responseDto.Data = response;
                responseDto.StatusCode = response != null ? (int)HttpStatusCode.Accepted : (int)HttpStatusCode.NoContent;
                responseDto.Status = true;
                responseDto.Message = response != null ? "Resquest is Successfull" : "the database is Empty";
                return responseDto;
            }
            catch (Exception Ex)
            {
                responseDto.Data = null;
                responseDto.StatusCode = (int)HttpStatusCode.BadRequest;
                responseDto.Status = false;
                responseDto.Message = "Resquest was unSuccessfull";
                responseDto.Error.Add(new ErrorItem() { InnerException = Ex.Message });
                return responseDto;
            }
        }
    }
}
