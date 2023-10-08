using API.DTOs.AccountRoles;
using API.DTOs.Educations;
using API.DTOs.Universities;
using API.Models;
using API.Utilites.Enums;
using System.Linq;

namespace API.DTOs.Accounts
{
    public class RegisterDto
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<AccountRoleDto> Roles { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }

        public static explicit operator RegisterDto(Employee employee)
        {
            {
                return new RegisterDto
                {
                    Guid = employee.Guid,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    BirthDate = employee.BirthDate,
                    Gender = employee.Gender,
                    HiringDate = employee.HiringDate,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Roles = employee.Account.AccountRoles.Select(x => (AccountRoleDto) x),
                    Password = employee.Account.Password,
                    Major = employee.Education.Major,
                    Degree = employee.Education.Degree,
                    Gpa = employee.Education.Gpa,
                    UniversityGuid = employee.Education.UniversityGuid
                };
            }
        }
    }
}
