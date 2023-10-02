using API.Contracts;
using API.Data;
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
            return Ok(result); // 200 dengan data Account
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
            return Ok(result); // 200 dengan isi data account
        }
        // Menginput Data Booking
        [HttpPost] // http method
        public IActionResult Create(Booking booking)
        {   // menambah data booking
            var result = _bookingRepository.Create(booking);
            if (result is null) // jika null variabel result
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }
            return Ok(result); //200 dengan data account
        }
        // Mengupdate Data Booking
        [HttpPut] // http method
        public IActionResult Update(Booking booking)
        {
            var result = _bookingRepository.Update(booking);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }
            return Ok(result); // 200
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
            return Ok(result);
        } 
    }
}
