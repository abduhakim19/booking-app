using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/employees")] //alamat url
    [Authorize(Roles = "admin")]
    public class EmployeeController : ControllerBase
    {   
        private readonly IEmployeeRepository _employeeRepository;
        // Inisialisasi untuk IEmployeeRepository (Contructor)
        public EmployeeController (IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        // Controller Get untuk mendapatkan semua data Account
        [HttpGet] //http method
        public IActionResult GetAll() 
        {
            try
            {
                var result = _employeeRepository.GetAll(); // dari repository untuk getAll
                if (!result.Any()) // menegecek ada data atau tidak
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                var data = result.Select(x => (EmployeeDto)x);

                return Ok(new ResponseOkHandler<IEnumerable<EmployeeDto>>("Success to retrieve data", data));
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
        // Controller Get Berdasarkan Guid /api/Employee/{guid}
        [HttpGet("{guid}")] //http method
        public IActionResult GetByGuid(Guid guid)
        {
            try
            {
                var result = _employeeRepository.GetByGuid(guid);
                if (result is null)
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                return Ok(new ResponseOkHandler<EmployeeDto>("Success to retrieve data", (EmployeeDto)result));
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
        // Menginput Data Employee
        [HttpPost] // http method
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            try
            {

                Employee toCreate = createEmployeeDto;
                
                toCreate.Nik = GenerateHandler.Nik(_employeeRepository.GetLastNik());

                var result = _employeeRepository.Create(toCreate);

                return Ok(new ResponseOkHandler<EmployeeDto>("Success to create data", (EmployeeDto)result));
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

        // Mengupdate Data Employee
        [HttpPut] // http method
        public IActionResult Update(EmployeeDto employeeDto)
        {
            try
            {   // GetByGuid dari database
                var employee = _employeeRepository.GetByGuid(employeeDto.Guid);
                if (employee is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                Employee toUpdate = employeeDto;
                toUpdate.CreatedDate = employee.CreatedDate;
                toUpdate.Nik = employee.Nik;

                var result = _employeeRepository.Update(toUpdate);

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
        // Menghapus Data Employee
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid) 
        {
            try
            {   // GetByGuid dari database
                var employee = _employeeRepository.GetByGuid(guid);
                if (employee is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                var result = _employeeRepository.Delete(employee);
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
