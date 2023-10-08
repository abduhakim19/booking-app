using API.Contracts;
using API.DTOs.Bookings;
using API.DTOs.Rooms;
using API.Models;
using API.Repositories;
using API.Utilites.Enums;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/rooms")] //alamat url
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        // Inisialisasi untuk IRoomRepository (Contructor)
        public RoomController(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }
        // Controller Get untuk mendapatkan semua data Room
        [HttpGet] //http method
        [Authorize(Roles = "admin")] // hanya admin
        public IActionResult GetAll()  
        {
            try
            {
                var result = _roomRepository.GetAll(); // dari repository untuk getAll
                if (!result.Any()) // menegecek ada data atau tidak
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                var data = result.Select(x => (RoomDto)x);

                return Ok(new ResponseOkHandler<IEnumerable<RoomDto>>("Success to retrieve data", data));
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
        // Controller Get Berdasarkan Guid /api/Room/{guid}
        [HttpGet("{guid}")] //http method
        [Authorize(Roles = "admin")] //hany admin
        public IActionResult GetByGuid(Guid guid) 
        { 
            try
            {
                var result = _roomRepository.GetByGuid(guid);
                if (result is null)
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                return Ok(new ResponseOkHandler<RoomDto>("Success to retrieve data", (RoomDto)result));
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
        
        [HttpGet("available")]
        [Authorize(Roles = "admin, user")] //admin dan user
        public IActionResult AvailableRooms()
        {
            // mendapatkan room yang dibooking
            var bookings = _bookingRepository.GetBookingByBeetweenStartAndEndDate(DateTime.Today)?
                .Where(b => b.Status == StatusLevel.OnBooking)
                .ToList();

            if (!bookings.Any())
            {
                throw new NotFoundHandler("Data Not Found");
            }
            // mendapatakan data room
            var rooms = _roomRepository.GetAll().ToList();
            //looping booking
            foreach (var booking in bookings)
            {   // menghapus room yang di booking
                rooms.RemoveAll(r => booking.RoomGuid == r.Guid);
            }
            var data = rooms.Select(x => (RoomDto) x);
            //return
            return Ok(new ResponseOkHandler<IEnumerable<RoomDto>>("Success to retrieve data", data));
        }

        // Menginput Data Room
        [HttpPost]  // http method
        [Authorize(Roles = "admin")] // hanya admin
        public IActionResult Create(CreateRoomDto createRoomDto)
        {   
            try
            {
                // menambah data room
                var result = _roomRepository.Create(createRoomDto);

                return Ok(new ResponseOkHandler<RoomDto>("Success to create data", (RoomDto)result));
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
        // Mengupdate Data Room
        [HttpPut] // http method
        [Authorize(Roles = "admin")] //hanya admin
        public IActionResult Update(RoomDto roomDto)
        {
            try
            {
                var room = _roomRepository.GetByGuid(roomDto.Guid);
                if (room is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                Room toUpdate = roomDto;
                toUpdate.CreatedDate = room.CreatedDate;

                var result = _roomRepository.Update(toUpdate);

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
        // Menghapus Data Room
        [HttpDelete] // http method
        [Authorize(Roles = "admin")] //hanya admin
        public IActionResult Delete(Guid guid) 
        { 
            try
            {
                var room = _roomRepository.GetByGuid(guid);
                if (room is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }
                var result = _roomRepository.Delete(room);

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
