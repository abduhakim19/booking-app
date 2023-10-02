using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //alamat url
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository _educationRepository;
        // Inisialisasi untuk IEducationRepository (Contructor)
        public EducationController(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }
        // Controller Get untuk mendapatkan semua data Education
        [HttpGet] //http method
        public IActionResult GetAll()
        {
            var result = _educationRepository.GetAll(); // dari repository untuk getAll
            if (!result.Any()) // mengecek ada data atau tidak
            {
                return NotFound("Data Not Found"); // 404 dengan pesan
            }
            return Ok(result); // 200 dengan data Account
        }
        // Controller Get Berdasarkan Guid /api/Education/{guid}
        [HttpGet("guid")] //http method
        public IActionResult GetByGuid(Guid guid) 
        {
            var result = _educationRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found"); // 404 dengan pesan
            }
            return Ok(result); // 200 dengan isi data account
        }
        // Menginput Data Education
        [HttpPost] // http method
        public IActionResult Create(Education education)
        {   // menambah data account
            var result = _educationRepository.Create(education);
            if (result is null) // jika null variabel result
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }
            return Ok(result); //200 dengan data education
        }
        // Mengupdate Data Education
        [HttpPut] // http method
        public IActionResult Update(Education education)
        {
            var result = _educationRepository.Update(education);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }
            return Ok(result);   // 200
        }
        // Menghapus Data Education
        [HttpDelete] // http method
        public IActionResult Delete(Education education) 
        { 
            var result = _educationRepository.Delete(education);
            if (!result) // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to delete data"); // 400 dengan pesan
            }
            return Ok(result); //200 berhasil
        }
    }
}
