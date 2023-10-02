using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //alamat url
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        // Inisialisasi untuk IAccountRoleRepository (Contructor)
        public AccountRoleController(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }
        // Controller Get untuk mendapatkan semua data AccountRole
        [HttpGet] //http method
        public IActionResult GetAll() 
        { 
            var result = _accountRoleRepository.GetAll(); // dari repository untuk getAll
            if (!result.Any()) // mengecek ada data atau tidak
            {
                return NotFound("Data not found"); // 404 dengan pesan
            }
            return Ok(result); // 200 dengan data Account
        }
        // Controller Get Berdasarkan Guid /api/AccountRole/{guid}
        [HttpGet("guid")] //http method
        public IActionResult GetByGuid(Guid guid) 
        {
            var result =  _accountRoleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id not found"); // 404 dengan pesan
            }
            return Ok(result); // 200 dengan isi data account
        }
        // Menginput Data AccountRole
        [HttpPost] // http method
        public IActionResult Create(AccountRole accountRole)
        {   // menambah data account
            var result = _accountRoleRepository.Create(accountRole);
            if (result is null) // jika null variabel result
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }
            return Ok(result);  //200 dengan data accountrole
        }
        // Mengupdate Data AccountRole
        [HttpPut] // http method
        public IActionResult Update(AccountRole accountRole)
        {
            var result = _accountRoleRepository.Update(accountRole);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }
            return Ok(result);  // 200
        }
        // Menghapus Data AccountRole
        [HttpDelete] // http method
        public IActionResult Delete(AccountRole accountRole)
        {
            var result = _accountRoleRepository.Delete(accountRole);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failde to delete data");  // 400 dengan pesan
            }
            return Ok(result); //200 berhasil
        } 
    }
}
