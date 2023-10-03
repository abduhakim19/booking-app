using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class EducationRepository inheritance interface IEducationRepository
    public class EducationRepository : GeneralRepository<Education> ,IEducationRepository
    {

        public EducationRepository(BookingManagementDbContext context) : base(context) { }
    }
}
