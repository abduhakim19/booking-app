using API.Contracts;
using API.Data;
using API.DTOs.Bookings;
using API.Models;
using Microsoft.AspNetCore.Mvc;

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
            var result = _bookingRepository.GetAll(); // dari repository untuk getAll
            if (!result.Any()) // mengecek ada data atau tidak
            {
                return NotFound("Data not found"); // 404 dengan pesan
            }
            var data = result.Select(x => (BookingDto)x);
            return Ok(data); // 200 dengan data Account
        }
        // Controller Get Berdasarkan Guid /api/Booking/{guid}
        [HttpGet("guid")] //http method
        public IActionResult GetByGuid(Guid guid) 
        { 
            var result = _bookingRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id not found"); // 404 dengan pesan
            }
            return Ok((BookingDto)result); // 200 dengan isi data account
        }
        // Menginput Data Booking
        [HttpPost] // http method
        public IActionResult Create(CreateBookingDto createBookingDto)
        {   // menambah data booking
            var result = _bookingRepository.Create(createBookingDto);
            if (result is null) // jika null variabel result
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }
            return Ok((BookingDto)result); //200 dengan data account
        }
        // Mengupdate Data Booking
        [HttpPut] // http method
        public IActionResult Update(BookingDto bookingDto)
        {
            var booking = _bookingRepository.GetByGuid(bookingDto.Guid);
            if (booking is null)
            {
                return NotFound("Id Not Found");
            }
            Booking toUpdate = bookingDto;
            toUpdate.CreatedDate = booking.CreatedDate;
            var result = _bookingRepository.Update(toUpdate);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }
            return Ok("Success to update data"); // 200
        }
        // Menghapus Data Booking
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid)
        {
     
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
                return NotFound("Id Not Found");

            var result = _bookingRepository.Delete(booking);
            if (!result)
                return BadRequest("Failed to delete data");
            
            return Ok("Success to delete data");
        } 
    }
}
