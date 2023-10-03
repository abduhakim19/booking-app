using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class UniversityRepository inheritance interface IUniversityRepository
    public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
    {   
        public UniversityRepository(BookingManagementDbContext context) : base(context) { }
    }
}
