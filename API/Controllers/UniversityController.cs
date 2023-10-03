using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Universities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // alamat url
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;
        // Inisialisasi untuk IUniversityRepository (Contructor)
        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }
        // Controller Get untuk mendapatkan semua data University
        [HttpGet] //http method
        public IActionResult GetAll()
        {
            var result = _universityRepository.GetAll(); // dari repository untuk getAll
            if (!result.Any()) // menegecek ada data atau tidak
            {
                return NotFound("Data Not Found"); // 400 dengan pesan
            }

            var data = result.Select(x => (UniversityDto) x);
            return Ok(data); // 200 dengan data university
        }
        // Controller Get Berdasarkan Guid /api/University/{guid}
        [HttpGet("{guid}")] //http method
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _universityRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found"); // 404 dengan pesan
            }
            return Ok((UniversityDto) result); // 200 dengan isi data university
        }
        // Menginput Data University
        [HttpPost] // http method
        public IActionResult Create(CreateUniversityDto createUniversityDto)
        {   // menambah data univeristy
            var result = _universityRepository.Create(createUniversityDto);
            if (result is null) // jika null variabel result
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }

            return Ok(result); //200 dengan data university
        }
        // Mengupdate Data University
        [HttpPut] // http method
        public IActionResult Update(UniversityDto universityDto)
        {
            var university = _universityRepository.GetByGuid(universityDto.Guid);
            if (university is null)
            {
                return NotFound("Id Not Found");
            }
            University toUpdate = universityDto;
            toUpdate.CreatedDate = university.CreatedDate;

            var result = _universityRepository.Update(toUpdate);
            if (!result) //  return result bool true jika berhasil maka memakai negasi jika gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }

            return Ok("Success to update data"); // 200
        }
        // Menghapus Data University
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid)
        {
            var university = _universityRepository.GetByGuid(guid);
            if (university is null)
                return NotFound("Id Not Found");
            var result = _universityRepository.Delete(university);

            if (!result) // kareana return bool true jika berhasil maka memakai negai jika gagal
                return BadRequest("Failed to delete data"); // 400 dengan pesan

            return Ok("Success to delete data"); //200 berhasil
        }
    }
}
