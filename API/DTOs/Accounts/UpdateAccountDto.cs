using API.Models;
using System.Security.Principal;

namespace API.DTOs.Accounts
{
    public class UpdateAccountDto
    {
        public string Password { get; set; }
        public Guid Guid { get; set; }
        public bool IsUsed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int Otp {  get; set; }


        public static implicit operator Account(UpdateAccountDto updateAccountDto)
        {
            return new Account
            {
                Guid = updateAccountDto.Guid,
                Password = updateAccountDto.Password,
                IsUsed = updateAccountDto.IsUsed,
                IsDeleted = updateAccountDto.IsDeleted,
                ExpiredTime = updateAccountDto.ExpiredTime,
                Otp = updateAccountDto.Otp
            };
        }
    }
}
