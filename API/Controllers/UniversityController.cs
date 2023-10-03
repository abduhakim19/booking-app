using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Universities;
using API.Utilities.Handlers;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // alamat url
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;
        // Inisialisasi untuk IUniversityRepository (Contructor)
        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }
        // Controller Get untuk mendapatkan semua data University
        [HttpGet] //http method
        public IActionResult GetAll()
        {
            try
            {
                var result = _universityRepository.GetAll(); // dari repository untuk getAll
                if (!result.Any()) // menegecek ada data atau tidak
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                var data = result.Select(x => (UniversityDto)x);

                return Ok(new ResponseOkHandler<IEnumerable<UniversityDto>>(data));
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
        // Controller Get Berdasarkan Guid /api/University/{guid}
        [HttpGet("{guid}")] //http method
        public IActionResult GetByGuid(Guid guid)
        {
            try
            {
                var result = _universityRepository.GetByGuid(guid);
                if (result is null)
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                return Ok(new ResponseOkHandler<UniversityDto>((UniversityDto)result));
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
        // Menginput Data University
        [HttpPost] // http method
        public IActionResult Create(CreateUniversityDto createUniversityDto)
        {   
            try
            {
                var result = _universityRepository.Create(createUniversityDto);

                return Ok(new ResponseOkHandler<UniversityDto>((UniversityDto)result));
            } catch (ExceptionHandler ex) 
            {   
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
            
        }
        // Mengupdate Data University
        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            try
            {   // GetByGuid dari database
                var university = _universityRepository.GetByGuid(universityDto.Guid);
                if (university is null) 
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                University toUpdate = universityDto;
                toUpdate.CreatedDate = university.CreatedDate;

                var result = _universityRepository.Update(toUpdate);

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
            catch(ExceptionHandler ex) // catchh ExceptionHanlder dari repository jika error
            {   // Return reponse dengan 500 internal ServerError
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
        }
        // Menghapus Data University
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid)
        {
            try
            {   // GetByGuid dari database
                var university = _universityRepository.GetByGuid(guid);
                if (university is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                var result = _universityRepository.Delete(university);
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
