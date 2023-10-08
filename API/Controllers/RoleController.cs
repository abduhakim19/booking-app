using API.Contracts;
using API.DTOs.Roles;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/roles")] //alamat url
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        // Inisialisasi untuk IRoleRepository (Contructor)
        public RoleController(IRoleRepository roleRepository) 
        { 
            _roleRepository = roleRepository;
        }
        // Controller Get untuk mendapatkan semua data Role
        [HttpGet] //http method
        public IActionResult GetAll() 
        {
            var result = _roleRepository.GetAll(); // dari repository untuk getAll
            if (!result.Any())  // mengecek ada data atau tidak
            {
                return NotFound("Data Not Found"); // 404 dengan pesan
            }
            var data = result.Select(x => (RoleDto) x);

            return Ok(new ResponseOkHandler<IEnumerable<RoleDto>>("Success to retrieve data", data));
        }
        // Controller Get Berdasarkan Guid /api/Role/{guid}
        [HttpGet("{guid}")] //http method
        public IActionResult GetByGuid(Guid guid) 
        { 
            try
            {
                var result = _roleRepository.GetByGuid(guid);
                if (result is null)
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }
                return Ok(new ResponseOkHandler<RoleDto>("Success to retrieve data", (RoleDto)result));
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
        // Menginput Data Role
        [HttpPost] // http method
        public IActionResult Create(CreateRoleDto roleDto)
        {
            try
            {
                var result = _roleRepository.Create(roleDto);

                return Ok(new ResponseOkHandler<RoleDto>("Success to create data", (RoleDto) result));
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
        // Mengupdate Data Role
        [HttpPut] // http method
        public IActionResult Update(RoleDto roleDto)
        {
            try
            {
                var role = _roleRepository.GetByGuid(roleDto.Guid);
                if (role is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }
                Role toUpdate = roleDto;
                toUpdate.CreatedDate = role.CreatedDate;

                var result = _roleRepository.Update(toUpdate);

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
        // Menghapus Data Role
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid)
        {
            try
            {   // GetByGuid dari database
                var role = _roleRepository.GetByGuid(guid);
                if (role is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                var result = _roleRepository.Delete(role);
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
