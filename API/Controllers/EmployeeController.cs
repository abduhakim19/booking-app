﻿using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //alamat url
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        // Inisialisasi untuk IEmployeeRepository (Contructor)
        public EmployeeController (IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        // Controller Get untuk mendapatkan semua data Account
        [HttpGet] //http method
        public IActionResult GetAll() 
        { 
            var result = _employeeRepository.GetAll(); // dari repository untuk getAll
            if (!result.Any()) // mengecek ada data atau tidak
            {
                return NotFound("Data Not Found"); // 404 dengan pesan
            }
            var data = result.Select(x => (EmployeeDto) x);
            return Ok(data); // 200 dengan data Employee
        }
        // Controller Get Berdasarkan Guid /api/Employee/{guid}
        [HttpGet("guid")] //http method
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _employeeRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found"); // 404 dengan pesan
            }
            return Ok((EmployeeDto)result); // 200 dengan data employee
        }
        // Menginput Data Employee
        [HttpPost] // http method
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {   // menambah data employee
            var result = _employeeRepository.Create(createEmployeeDto);
            if (result is null) // jika null variabel result
            {
                return BadRequest("Failed to create data"); // 400 dengan pesan
            }
            return Ok((EmployeeDto)result); //200 dengan data employee
        }
        // Mengupdate Data Employee
        [HttpPut] // http method
        public IActionResult Update(EmployeeDto employeeDto)
        {
            var employee = _employeeRepository.GetByGuid(employeeDto.Guid);
            if (employee is null)
            {
                return NotFound("Id Not Found");
            }

            Employee toUpdate = employeeDto;
            toUpdate.CreatedDate = employee.CreatedDate;
            var result = _employeeRepository.Update(toUpdate);
            if (!result)  // return result bool true jika berhasil maka memakai negasi untuk gagal
            {
                return BadRequest("Failed to update data"); // 400 dengan pesan
            }
            return Ok("Success to update data"); // 200
        }
        // Menghapus Data Employee
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid) 
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
                return NotFound("Id Not Found");
            var result = _employeeRepository.Delete(employee);
            if (!result)
                return BadRequest("Failed to delete data");
            
            return Ok("Success to delete data");
        } 
    }
}
