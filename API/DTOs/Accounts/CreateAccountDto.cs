
using API.Models;

namespace API.DTOs.Accounts
{
    public class CreateAccountDto
    {
        public string Password { get; set; }
        public Guid Guid { get; set; }

        public static implicit operator Account(CreateAccountDto createAccountDto)
        {
            return new Account
            {
                Guid = createAccountDto.Guid,
                Password = createAccountDto.Password,
                IsDeleted = false,
                IsUsed = false,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
