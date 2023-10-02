using API.Models;

namespace API.Contracts
{   // Interface  EducationRepository
    public interface IEducationRepository
    {
        IEnumerable<Education> GetAll(); // return Ienumerable
        Education? GetByGuid(Guid guid); // return objek Education boleh null
        Education? Create(Education education); // return objek Education boleh null
        bool Update(Education education); // return bool
        bool Delete(Education education); // return bool 
    }
}
