using API.DTOs.Universities;
using API.Models;
using API.Utilites.Enums;

namespace API.DTOs.Accounts
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public string Password { get; set; }

        public static explicit operator RegisterDto(CreateRegisterDto registerDto)
        {
            return new RegisterDto
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                HiringDate = registerDto.HiringDate,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                    Major = registerDto.Major,
                    Degree = registerDto.Degree,
                    Gpa = registerDto.Gpa,
                    UniversityCode = registerDto.UniversityCode,
                    UniversityName = registerDto.UniversityName,
                    Password = registerDto.Password
            };
        }
    }
}
