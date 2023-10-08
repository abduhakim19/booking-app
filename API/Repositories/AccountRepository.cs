using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.DTOs.Educations;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repositories
{   // class AccountRepository inheritance interface IAccountRepository
    // Isi Method sesuai interface
    public class AccountRepository: GeneralRepository<Account>, IAccountRepository
    {   
        private readonly BookingManagementDbContext _context;
        public AccountRepository(BookingManagementDbContext context) : base(context)
        {
           _context = context;
        }

        public Employee? GetEmployeeAndAccountByEmail(string email)
        {   // Get employee and account by email
            var employeeAccount = _context.Set<Employee>()
                .Join(_context.Accounts, e => e.Guid, a => a.Guid, (e, a) => new Employee
            {
               Guid = e.Guid,
               Email = e.Email,
               FirstName = e.FirstName,
               LastName = e.LastName,
               Nik = e.Nik,
               Account = new Account
               {
                   Otp = a.Otp,
                   Password = a.Password,
                   IsUsed = a.IsUsed,
                   ExpiredTime = a.ExpiredTime
               }
            })
                    .Where(x => x.Email == email)
                    .FirstOrDefault();
            return employeeAccount;
        }
        // melakukan register ke database
        public Employee? Register(Employee employee) 
        {
            var transaction = _context.Database.BeginTransaction(); // transaction
            try
            {
                _context.AddRange(employee); //simpan data
                _context.SaveChanges(); // rollback automation
                
                transaction.Commit();
                return employee;

            } catch (Exception ex)
            {
                transaction.Rollback();
                throw new ExceptionHandler(ex.Message);
            }
        }
    }
}
