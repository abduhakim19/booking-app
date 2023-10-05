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

        public EmployeeAndAccountDto? GetEmployeeAndAccountByEmail(string email)
        {   // Get employee and account by email
            var employeeAccount = _context.Set<Employee>()
                .Join(_context.Accounts, e => e.Guid, a => a.Guid, (e, a) => new EmployeeAndAccountDto
            {
               Guid = e.Guid,
               Email = e.Email,
               FirstName = e.FirstName,
               LastName = e.LastName,
               Nik = e.Nik,
               Otp = a.Otp,
               Password = a.Password,
               IsUsed = a.IsUsed,
               ExpiredTime = a.ExpiredTime
            })
                    .Where(x => x.Email == email)
                    .FirstOrDefault();
            return employeeAccount;
        }
        // melakukan register ke database
        public CreateRegisterDto? Register(CreateRegisterDto registerDto, Guid universityGuid, string nik) 
        {
            var transaction = _context.Database.BeginTransaction(); // transaction
            try
            {   // menimpan data berdasarkan objek yang berelasi di ef core
                var insert = new Employee
                    {
                        FirstName = registerDto.FirstName,
                        LastName = registerDto.LastName,
                        BirthDate = registerDto.BirthDate,
                        Gender = registerDto.Gender,
                        HiringDate = registerDto.HiringDate,
                        Email = registerDto.Email,
                        Nik = nik,
                        PhoneNumber = registerDto.PhoneNumber,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Education = new CreateEducationDto()
                        {
                            Major = registerDto.Major,
                            Degree = registerDto.Degree,
                            Gpa = registerDto.Gpa,
                            UniversityGuid = universityGuid
                        },
                        Account = new CreateAccountDto()
                        {
                            Password = registerDto.Password
                        }
                    };
                    _context.AddRange(insert); //simpan data
                    _context.SaveChanges();


                    transaction.Commit();
                    return registerDto;

            } catch (Exception ex)
            {
                throw new ExceptionHandler(ex.Message);
            }
        }
    }
}
