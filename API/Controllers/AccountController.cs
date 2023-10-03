using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Rooms;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

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
            var data = result.Select(x => (AccountDto)x);
            return Ok(data); // 200 dengan data Account
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
            return Ok((AccountDto)result); // 200 dengan isi data account
        }
        // Menginput Data Account
        [HttpPost] // http method
        public IActionResult Create(CreateAccountDto createAccountDto)
        {   // menambah data account
            var result = _accountRepository.Create(createAccountDto);
            if (result is null) // jika null variabel result
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }
            return Ok((AccountDto) result); //200 dengan data account
        }
        // Mengupdate Data Account
        [HttpPut] // http method
        public IActionResult Update(UpdateAccountDto updateAccountDto)
        {
            var account = _accountRepository.GetByGuid(updateAccountDto.Guid);
            if (account is null)
            {
                return NotFound("Id Not Found");
            }
            Account toUpdate = updateAccountDto;
            toUpdate.CreatedDate = account.CreatedDate;
            var result = _accountRepository.Update(toUpdate);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }

            return Ok("Success to update data"); // 200
        }
        // Menghapus Data Account
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
                return NotFound("Id Not Found");
            var result = _accountRepository.Delete(account);
            if (!result)  // return result bool true jika berhasil maka memakai negasi untuk gagal
                return BadRequest("Failed to delete data"); // 400 dengan pesan
            
            return Ok("Success to delete data"); //200 berhasil
        }
    }
}
