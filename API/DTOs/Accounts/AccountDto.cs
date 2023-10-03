using API.Models;
using System.Security.Principal;

namespace API.DTOs.Accounts
{
    public class AccountDto
    {
        public Guid Guid { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ExpiredTime { get; set; }


        public static explicit operator AccountDto(Account account)
        {
            return new AccountDto
            {
                Guid = account.Guid,
                IsDeleted = account.IsDeleted,
                ExpiredTime = account.ExpiredTime,
            };
        }

        public static implicit operator Account(AccountDto accountDto)
        {
            return new Account
            {
                Guid = accountDto.Guid,
                IsDeleted = accountDto.IsDeleted,
                ExpiredTime = accountDto.ExpiredTime,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
