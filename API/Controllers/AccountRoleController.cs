using API.Contracts;
using API.DTOs.AccountRoles;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/account-roles")] //alamat url
    [Authorize(Roles = "admin")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        // Inisialisasi untuk IAccountRoleRepository (Contructor)
        public AccountRoleController(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }
        // Controller Get untuk mendapatkan semua data AccountRole
        [HttpGet] //http method
        public IActionResult GetAll() 
        {
            try
            {
                var result = _accountRoleRepository.GetAll(); // dari repository untuk getAll
                if (!result.Any()) // menegecek ada data atau tidak
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                var data = result.Select(x => (AccountRoleDto)x);

                return Ok(new ResponseOkHandler<IEnumerable<AccountRoleDto>>("Success to retrieve data", data));
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
        // Controller Get Berdasarkan Guid /api/AccountRole/{guid}
        [HttpGet("{guid}")] //http method
        public IActionResult GetByGuid(Guid guid) 
        {
            try
            {
                var result = _accountRoleRepository.GetByGuid(guid);
                if (result is null)
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                return Ok(new ResponseOkHandler<AccountRoleDto>("Success to retrieve data", (AccountRoleDto)result));
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
        // Menginput Data AccountRole
        [HttpPost] // http method
        public IActionResult Create(CreateAccountRoleDto createAccountRoleDto)
        {   // menambah data account
            try
            {
                var result = _accountRoleRepository.Create(createAccountRoleDto);

                return Ok(new ResponseOkHandler<AccountRoleDto>("Success to create data", (AccountRoleDto) result));
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
        // Mengupdate Data AccountRole
        [HttpPut] // http method
        public IActionResult Update(AccountRoleDto accountRoleDto)
        {
            try
            {   // GetByGuid dari database
                var accountRole = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
                if (accountRole is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                AccountRole toUpdate = accountRoleDto;
                toUpdate.CreatedDate = accountRole.CreatedDate;

                var result = _accountRoleRepository.Update(toUpdate);

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
        // Menghapus Data AccountRole
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid)
        {
            try
            {   // GetByGuid dari database
                var accountRole = _accountRoleRepository.GetByGuid(guid);
                if (accountRole is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                var result = _accountRoleRepository.Delete(accountRole);
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
