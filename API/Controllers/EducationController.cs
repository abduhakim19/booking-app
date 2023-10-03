using API.Contracts;
using API.DTOs.Educations;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //alamat url
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository _educationRepository;
        // Inisialisasi untuk IEducationRepository (Contructor)
        public EducationController(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }
        // Controller Get untuk mendapatkan semua data Education
        [HttpGet] //http method
        public IActionResult GetAll()
        {
            try
            {
                var result = _educationRepository.GetAll(); // dari repository untuk getAll
                if (!result.Any()) // menegecek ada data atau tidak
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                var data = result.Select(x => (EducationDto)x);

                return Ok(new ResponseOkHandler<IEnumerable<EducationDto>>(data));
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
        // Controller Get Berdasarkan Guid /api/Education/{guid}
        [HttpGet("guid")] //http method
        public IActionResult GetByGuid(Guid guid) 
        {
            try
            {
                var result = _educationRepository.GetByGuid(guid);
                if (result is null)
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                return Ok(new ResponseOkHandler<EducationDto>((EducationDto)result));
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
        // Menginput Data Education
        [HttpPost] // http method
        public IActionResult Create(CreateEducationDto createEducationDto)
        {
            try
            {
                var result = _educationRepository.Create(createEducationDto);

                return Ok(new ResponseOkHandler<EducationDto>((EducationDto)result));
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
        // Mengupdate Data Education
        [HttpPut] // http method
        public IActionResult Update(EducationDto educationDto)
        {
            try
            {   // GetByGuid dari database
                var education = _educationRepository.GetByGuid(educationDto.Guid);
                if (education is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                Education toUpdate = educationDto;
                toUpdate.CreatedDate = education.CreatedDate;

                var result = _educationRepository.Update(toUpdate);

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
        // Menghapus Data Education
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid) 
        {
            try
            {   // GetByGuid dari database
                var education = _educationRepository.GetByGuid(guid);
                if (education is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                var result = _educationRepository.Delete(education);
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
