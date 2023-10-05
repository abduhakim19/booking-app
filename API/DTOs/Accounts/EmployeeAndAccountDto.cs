using API.Utilites.Enums;

namespace API.DTOs.Accounts
{
    public class EmployeeAndAccountDto
    {
        public Guid Guid { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string Password { get; set; }
        public string Nik { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
    }
}
