using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //alamat url
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        // Inisialisasi untuk IAccountRepository (Contructor)
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        // Controller Get untuk mendapatkan semua data Account
        [HttpGet] //http method
        public IActionResult GetAll() 
        {
            try
            {
                var result = _accountRepository.GetAll(); // dari repository untuk getAll
                if (!result.Any()) // menegecek ada data atau tidak
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                var data = result.Select(x => (AccountDto)x);

                return Ok(new ResponseOkHandler<IEnumerable<AccountDto>>(data));
            }
            catch (NotFoundHandler ex)
            {
                // Return Response 404 Not Found
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = ex.Message
                });
            }
        }

        // Controller Get Berdasarkan Guid /api/Account/{guid}
        [HttpGet("guid")] //http method
        public IActionResult GetByGuid(Guid guid) 
        {
            try
            {
                var result = _accountRepository.GetByGuid(guid);
                if (result is null)
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                return Ok(new ResponseOkHandler<AccountDto>((AccountDto)result));
            }
            catch (NotFoundHandler ex)
            {
                // Return Response 404 Not Found
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = ex.Message
                });
            }
        }
        // Menginput Data Account
        [HttpPost] // http method
        public IActionResult Create(CreateAccountDto createAccountDto)
        {   // menambah data account
            try
            {
                var result = _accountRepository.Create(createAccountDto);

                return Ok(new ResponseOkHandler<AccountDto>((AccountDto)result));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
        }
        // Mengupdate Data Account
        [HttpPut] // http method
        public IActionResult Update(AccountDto accountDto)
        {
            try
            {   // GetByGuid dari database
                var account = _accountRepository.GetByGuid(accountDto.Guid);
                if (account is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                Account toUpdate = accountDto;
                toUpdate.CreatedDate = account.CreatedDate;

                var result = _accountRepository.Update(toUpdate);

                return Ok(new ResponseOkHandler<string>("Data Updated"));

            }
            catch (NotFoundHandler ex)
            {
                // Return Response 404 Not Found
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = ex.Message
                });
            }
            catch (ExceptionHandler ex) // catchh ExceptionHanlder dari repository jika error
            {   // Return reponse dengan 500 internal ServerError
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
        }
        // Menghapus Data Account
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid)
        {
            try
            {   // GetByGuid dari database
                var account = _accountRepository.GetByGuid(guid);
                if (account is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                var result = _accountRepository.Delete(account);
                return Ok(new ResponseOkHandler<string>("Data Deleted"));

            }
            catch (NotFoundHandler ex)
            {
                // Return Response 404 Not Found
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = ex.Message
                });
            }
            catch (ExceptionHandler ex) // catchh ExceptionHanlder dari repository jika error
            {   // Return reponse dengan 500 internal ServerError
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
        }
    }
}
