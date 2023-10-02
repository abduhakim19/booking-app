using API.Models;

namespace API.Contracts
{   // Intewrface EmployeeRepository
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll(); // return Ienumerable
        Employee? GetByGuid(Guid guid); // return objek  Employee boleh null
        Employee? Create(Employee employee); // return objek Employee boleh null
        bool Update(Employee employee); // return bool
        bool Delete(Employee employee); // return bool 
    }
}
