using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //alamat url
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        // Inisialisasi untuk IRoomRepository (Contructor)
        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        // Controller Get untuk mendapatkan semua data Room
        [HttpGet] //http method
        public IActionResult GetAll()  
        {
            var result = _roomRepository.GetAll(); // dari repository untuk getAll
            if (!result.Any()) // mengecek ada data atau tidak
            {
                return NotFound("Data Not Found"); // 404 dengan pesan
            }
            return Ok(result); // 200 dengan data Account
        }
        // Controller Get Berdasarkan Guid /api/Room/{guid}
        [HttpGet("guid")] //http method
        public IActionResult GetByGuid(Guid guid) 
        { 
            var result = _roomRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found"); // 404 dengan pesan
            }
            return Ok(result); // 200 dengan isi data room
        }
        // Menginput Data Room
        [HttpPost]  // http method
        public IActionResult Create(Room room)
        {   // menambah data room
            var result = _roomRepository.Create(room);
            if (result is null) // jika null variabel result
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }
            return Ok(result); //200 dengan data account
        }
        // Mengupdate Data Room
        [HttpPut] // http method
        public IActionResult Update(Room room)
        {
            var result = _roomRepository.Update(room);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }
            return Ok(result); // 200
        }
        // Menghapus Data Room
        [HttpDelete] // http method
        public IActionResult Delete(Room room) 
        { 
            var result = _roomRepository.Delete(room);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to delete data"); // 400 dengan pesan
            }
            return Ok(result); //200 berhasil
        }
    }
}
