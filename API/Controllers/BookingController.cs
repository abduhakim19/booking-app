using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.DTOs.Bookings;
using API.Models;
using API.Repositories;
using API.Utilites.Enums;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/bookings")] //alamat url
    [Authorize(Roles = "admin, user")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IEmployeeRepository _employeeRepository;
        // Inisialisasi untuk IBookingRepository (Contructor)
        public BookingController(IBookingRepository bookingRepository, IRoomRepository roomRepository, IEmployeeRepository employeeRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _employeeRepository = employeeRepository;
        }
        // Controller Get untuk mendapatkan semua data Booking
        [HttpGet] //http method
        public IActionResult GetAll() 
        {
            try
            {
                var result = _bookingRepository.GetAll();

                if (!result.Any()) // menegecek ada data atau tidak
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                var data = result.Select(x => (BookingDto)x);

                return Ok(new ResponseOkHandler<IEnumerable<BookingDto>>("Success to create data", data));
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

        [HttpGet("details")]
        public IActionResult GetAllDetail()
        {
            try
            {   // mendapatkan data bookins, employee, dan rooms
                var bookings = _bookingRepository.GetAll();
                var employees = _employeeRepository.GetAll();
                var rooms = _roomRepository.GetAll();
                // melakukan join booking, employee dan rooms
                var result = bookings
                                .Join(employees, booking => booking.EmployeeGuid, emp => emp.Guid,
                                    (booking, emp) => new { booking, emp })
                                .Join(rooms, bookingEmp => bookingEmp.booking.RoomGuid, room => room.Guid, (bookingEmp, room) => new BookingDetailDto{
                                    Guid = bookingEmp.booking.Guid,
                                    BookedNik = bookingEmp.emp.Nik,
                                    BookedBy = bookingEmp.emp.FirstName + " " + bookingEmp.emp.LastName,
                                    RoomName = room.Name,
                                    StartDate = bookingEmp.booking.StartDate,
                                    EndDate = bookingEmp.booking.EndDate,
                                    Status = bookingEmp.booking.Status.ToString(),
                                    Remarks = bookingEmp.booking.Remarks
                                }).ToList();

                if (!result.Any()) // menegecek ada data atau tidak
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }
                return Ok(new ResponseOkHandler<IEnumerable<BookingDetailDto>>("Success to retrieve data", result)); //return
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

        [HttpGet("details/{guid}")]
        public IActionResult GetAllDetailByGuid(Guid guid)
        {
            try
            {
                // mendapatkan data berdasarkan guid
                var booking = _bookingRepository.GetByGuid(guid);
                var employee = _employeeRepository.GetByGuid(booking.EmployeeGuid);
                var room = _roomRepository.GetByGuid(booking.RoomGuid);
                // cek data
                if (booking is null || employee is null || room is null) // menegecek ada data null atau tidak
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }
                // merekontruksi data
                var result = new BookingDetailDto
                {
                    Guid = booking.Guid,
                    BookedNik = employee.Nik,
                    BookedBy = employee.FirstName + " " + employee.LastName,
                    RoomName = room.Name,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,
                    Status = booking.Status.ToString(),
                   Remarks = booking.Remarks
                };
                
                // return
                return Ok(new ResponseOkHandler<BookingDetailDto>("Success to retrieve data", result));
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

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            try
            {
                var result = _bookingRepository.GetByGuid(guid);

                if (result is null)
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                return Ok(new ResponseOkHandler<BookingDto>("Success to retrieve data", (BookingDto)result));
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

        [HttpGet("booking-length/{guid}")]
        public IActionResult GetBookingLengthByGuid(Guid guid)
        {
            try
            {   // Get Booking Data
                var booking = _bookingRepository.GetByGuid(guid);
                if (booking is null)
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }
                // Menghitung Lama booking tanpa weekend
                var totalDay = WeekDayDateHandler.Calculate(booking.StartDate, booking.EndDate);
                var room = _roomRepository.GetByGuid(booking.RoomGuid);
                BookingLengthDto bookingLengthDto = (BookingLengthDto)booking;
                bookingLengthDto.BookingLength = totalDay;
                bookingLengthDto.RoomName = room.Name;

                return Ok(new ResponseOkHandler<BookingLengthDto>("Success to retrieve data", bookingLengthDto));
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

        [HttpGet("booked-room")]
        public IActionResult GetBookingRoomToday() 
        {
            try
            {   // mendapatakan data booking berdasarkan tanggan diantara dan dengan status on booking
                var bookings = _bookingRepository.GetBookingByBeetweenStartAndEndDate(DateTime.Today)?.Where(b => b.Status == StatusLevel.OnBooking);

                if (!bookings.Any())
                {
                    throw new NotFoundHandler("Data Not Found");
                }
                // melakukan resturctru untuk output
                var dataBookingRoomToday = new List<BookingRoomTodayDto>();
                foreach (var booking in bookings)
                {
                    var room = _roomRepository.GetByGuid(booking.RoomGuid);
                    var employee = _employeeRepository.GetByGuid(booking.EmployeeGuid);
                    dataBookingRoomToday.Add(new BookingRoomTodayDto
                    {
                        BookingGuid = booking.Guid,
                        RoomName = room.Name,
                        Status = booking.Status.ToString(),
                        Floor = room.Floor,
                        BookedBy = employee.FirstName + " " + employee.LastName,
                    });
                }
                //return 
                return Ok(new ResponseOkHandler<IEnumerable<BookingRoomTodayDto>>("Success to retrieve data", dataBookingRoomToday));
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
        // Controller Get Berdasarkan Guid /api/Booking/{guid}
        
        // Menginput Data Booking
        [HttpPost] // http method
        public IActionResult Create(CreateBookingDto createBookingDto)
        {
            try
            {
                var result = _bookingRepository.Create(createBookingDto);

                return Ok(new ResponseOkHandler<BookingDto>("Success to create data", (BookingDto) result));
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
