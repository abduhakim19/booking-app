using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //alamat url
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
            return Ok(result); // 200 dengan data Role
        }
        // Controller Get Berdasarkan Guid /api/Role/{guid}
        [HttpGet("guid")] //http method
        public IActionResult GetByGuid(Guid guid) 
        { 
            var result = _roleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found"); // 404 dengan pesan
            }
            return Ok(result); // 200 dengan isi data role
        }
        // Menginput Data Role
        [HttpPost] // http method
        public IActionResult Create(Role role)
        {   // menambah data role
            var result = _roleRepository.Create(role);
            if (result is null) // jika null variabel result
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }
            return Ok(result); //200 dengan data account
        }
        // Mengupdate Data Role
        [HttpPut] // http method
        public IActionResult Update(Role role)
        {
            var result = _roleRepository.Update(role);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }
            return Ok(result); // 200
        }
        // Menghapus Data Role
        [HttpDelete] // http method
        public IActionResult Delete(Role role)
        {
            var result = _roleRepository.Delete(role);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to delete data"); // 400 dengan pesan
            }
            return Ok(result); //200 berhasil
        }
    }
}
