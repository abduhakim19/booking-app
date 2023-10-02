using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //alamat url
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        // Inisialisasi untuk IAccountRepository (Contructor)
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        // Controller Get untuk mendapatkan semua data Account
        [HttpGet] //http method
        public IActionResult GetAll() 
        { 
            var result = _accountRepository.GetAll(); // dari repository untuk getAll
            if (!result.Any()) // mengecek ada data atau tidak
            {
                return NotFound("Data Not Found"); // 404 dengan pesan
            }
            return Ok(result); // 200 dengan data Account
        }

        // Controller Get Berdasarkan Guid /api/Account/{guid}
        [HttpGet("guid")] //http method
        public IActionResult GetByGuid(Guid guid) 
        {
            var result = _accountRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found"); // 404 dengan pesan
            }
            return Ok(result); // 200 dengan isi data account
        }
        // Menginput Data Account
        [HttpPost] // http method
        public IActionResult Create(Account account)
        {   // menambah data account
            var result = _accountRepository.Create(account);
            if (result is null) // jika null variabel result
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }
            return Ok(result); //200 dengan data account
        }
        // Mengupdate Data Account
        [HttpPut] // http method
        public IActionResult Update(Account account)
        {
            var result = _accountRepository.Update(account);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }

            return Ok(result); // 200
        }
        // Menghapus Data Account
        [HttpDelete] // http method
        public IActionResult Delete(Account account)
        {
            var result = _accountRepository.Delete(account);
            if (!result)  // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to delete data"); // 400 dengan pesan
            }
            return Ok(result); //200 berhasil
        }
    }
}
