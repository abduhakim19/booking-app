using API.Contracts;
using API.Data;
using API.DTOs.Bookings;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //alamat url
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        // Inisialisasi untuk IBookingRepository (Contructor)
        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        // Controller Get untuk mendapatkan semua data Booking
        [HttpGet] //http method
        public IActionResult GetAll() 
        {
            try
            {
                var result = _bookingRepository.GetAll(); // dari repository untuk getAll
                if (!result.Any()) // menegecek ada data atau tidak
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                var data = result.Select(x => (BookingDto)x);

                return Ok(new ResponseOkHandler<IEnumerable<BookingDto>>(data));
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
        // Controller Get Berdasarkan Guid /api/Booking/{guid}
        [HttpGet("guid")] //http method
        public IActionResult GetByGuid(Guid guid) 
        {
            try
            {
                var result = _bookingRepository.GetByGuid(guid);
                if (result is null)
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                return Ok(new ResponseOkHandler<BookingDto>((BookingDto)result));
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
        // Menginput Data Booking
        [HttpPost] // http method
        public IActionResult Create(CreateBookingDto createBookingDto)
        {
            try
            {
                var result = _bookingRepository.Create(createBookingDto);

                return Ok(new ResponseOkHandler<BookingDto>((BookingDto)result));
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
        // Mengupdate Data Booking
        [HttpPut] // http method
        public IActionResult Update(BookingDto bookingDto)
        {
            try
            {   // GetByGuid dari database
                var booking = _bookingRepository.GetByGuid(bookingDto.Guid);
                if (booking is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                Booking toUpdate = bookingDto;
                toUpdate.CreatedDate = booking.CreatedDate;

                var result = _bookingRepository.Update(toUpdate);

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
        // Menghapus Data Booking
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid)
        {
            try
            {   // GetByGuid dari database
                var booking = _bookingRepository.GetByGuid(guid);
                if (booking is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                var result = _bookingRepository.Delete(booking);
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
