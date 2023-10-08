using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.Models;

namespace API.Contracts
{   // Interface untuk AccountRepository
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        Employee? GetEmployeeAndAccountByEmail(string email);

        Employee? Register(Employee employee);

        
    }
}
