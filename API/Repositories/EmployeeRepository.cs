using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Repositories
{
    // class EmployeeRepository inheritance interface IEmployeeRepository
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {   // properti DbContext
        private readonly BookingManagementDbContext _context;
        public EmployeeRepository(BookingManagementDbContext context) : base(context) 
        {
            _context = context;
        }
        public string GetLastNik()
        {
            var entity = _context.Set<Employee>().OrderByDescending(e => e.Nik).FirstOrDefault()?.Nik ?? null;
            _context.ChangeTracker.Clear();
            return entity;
        }

        public Employee? GetByEmail(string email)
        {
            var data = _context.Set<Employee>().Where(x => x.Email == email).FirstOrDefault();
            _context.ChangeTracker.Clear();

            return data;
        }
    }
}
