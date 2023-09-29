using API.Models;

namespace API.Contracts
{   // Interface untuk UniversityRepository atau Contracts
    public interface IUniversityRepository
    {   
        IEnumerable<University> GetAll(); // return Ienumerable
        University? GetByGuid(Guid guid); // return objek/class University boleh null
        University? Create(University university); // return objek/class University boleh null
        bool Update(University university); // return bool
        bool Delete(University university); // return bool 
    }
}
