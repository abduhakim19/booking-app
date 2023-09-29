﻿using API.Models;
using API.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }
        // Controller Get Berdasarkan Id /api/University/{guid}
        [HttpGet("{guid}")] //http method
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _universityRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found"); // 404 dengan pesan
            }
            return Ok(result); // 200 dengan isi data
        }
        // Menginput Data University
        [HttpPost] // http method
        public IActionResult Create(University university)
        {
            var result = _universityRepository.Create(university);
            if (result is null)
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }

            return Ok(result); //200 
        }
        // Mengupdate Data University
        [HttpPut] // http method
        public IActionResult Update(University university)
        {   
            var result = _universityRepository.Update(university);
            if (!result) // kareana return bool true jika berhasil maka memakai negai jika gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }

            return Ok(result); // 200
        }
        // Menghapus Data University
        [HttpDelete] // http method
        public IActionResult Delete(University university)
        {
            var result = _universityRepository.Delete(university);
            if (!result) // kareana return bool true jika berhasil maka memakai negai jika gagal
            {
                return BadRequest("Failed to delete data"); // 400 dengan pesan
            }

            return Ok(result); //200 berhasil
        }
    }
}