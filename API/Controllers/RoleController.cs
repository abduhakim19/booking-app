using API.Contracts;
using API.DTOs.Roles;
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
            var data = result.Select(x => (RoleDto) x);

            return Ok(data); // 200 dengan data Role
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
            return Ok((RoleDto) result); // 200 dengan isi data role
        }
        // Menginput Data Role
        [HttpPost] // http method
        public IActionResult Create(CreateRoleDto roleDto)
        {   // menambah data role
            var result = _roleRepository.Create(roleDto);
            if (result is null) // jika null variabel result
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }
            return Ok((RoleDto)result); //200 dengan data account
        }
        // Mengupdate Data Role
        [HttpPut] // http method
        public IActionResult Update(RoleDto roleDto)
        {
            var role = _roleRepository.GetByGuid(roleDto.Guid);
            if (role is null)
            {
                return NotFound("Id Not Found");
            }
            Role toUpdate = roleDto;
            toUpdate.CreatedDate = role.CreatedDate;
            var result = _roleRepository.Update(toUpdate);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }
            return Ok("Success to update data"); // 200
        }
        // Menghapus Data Role
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role is null)
                return NotFound("Id Not Found");
            var result = _roleRepository.Delete(role);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
                return BadRequest("Failed to delete data"); // 400 dengan pesan

            return Ok("Success to delete data"); //200 berhasil
        }
    }
}
