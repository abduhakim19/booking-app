using API.Models;

namespace API.Contracts
{   // Intewrface EmployeeRepository
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        string GetLastNik();
    }
}
