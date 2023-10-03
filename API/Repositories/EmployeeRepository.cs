using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // class EmployeeRepository inheritance interface IEmployeeRepository
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {   // properti DbContext
        public EmployeeRepository(BookingManagementDbContext context) : base(context) { }
    }
}
