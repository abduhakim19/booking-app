using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.Models;

namespace API.Contracts
{   // Interface untuk AccountRepository
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        EmployeeAndAccountDto? GetEmployeeAndAccountByEmail(string email);

        CreateRegisterDto? Register(CreateRegisterDto registerDto, Guid guidUniversity, string nik);
    }
}
